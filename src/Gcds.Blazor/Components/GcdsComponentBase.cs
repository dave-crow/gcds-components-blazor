using System.Collections;
using System.Globalization;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;
using Gcds.Blazor.Interop;

namespace Gcds.Blazor.Components;

public abstract class GcdsComponentBase : ComponentBase, IAsyncDisposable
{
    private IJSObjectReference? _module;
    private DotNetObjectReference<GcdsComponentBase>? _self;
    private bool _disposed;
    private HashSet<string> _configuredPropertyNames = new(StringComparer.Ordinal);

    [Inject] protected IJSRuntime JS { get; set; } = default!;
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public IReadOnlyDictionary<string, object?>? AdditionalAttributes { get; set; }
    [Parameter] public IReadOnlyDictionary<string, object?>? Properties { get; set; }
    [Parameter] public EventCallback<GcdsEventArgs> OnGcdsEvent { get; set; }
    [Parameter] public EventCallback<GcdsEventArgs> OnGcdsDismiss { get; set; }
    [Parameter] public EventCallback<GcdsEventArgs> OnGcdsClick { get; set; }
    [Parameter] public EventCallback<GcdsEventArgs> OnGcdsFocus { get; set; }
    [Parameter] public EventCallback<GcdsEventArgs> OnGcdsBlur { get; set; }
    [Parameter] public EventCallback<GcdsEventArgs> OnGcdsInput { get; set; }
    [Parameter] public EventCallback<GcdsEventArgs> OnGcdsChange { get; set; }
    [Parameter] public EventCallback<GcdsEventArgs> OnGcdsError { get; set; }
    [Parameter] public EventCallback<GcdsEventArgs> OnGcdsValid { get; set; }
    [Parameter] public EventCallback<GcdsEventArgs> OnGcdsSubmit { get; set; }
    [Parameter] public EventCallback<GcdsEventArgs> OnGcdsRemoveFile { get; set; }
    [Parameter] public EventCallback<GcdsEventArgs> OnGcdsSuggestionSelected { get; set; }
    [Parameter] public EventCallback<GcdsEventArgs> OnGcdsTableStateChange { get; set; }
    [Parameter] public IReadOnlyCollection<string>? AdditionalEventNames { get; set; }

    public ElementReference Element { get; private set; }
    protected abstract string TagName { get; }
    protected virtual IReadOnlyCollection<string> EventNames => GcdsInterop.DefaultEvents;

    private IReadOnlyCollection<string> GetEventNames()
    {
        if (AdditionalEventNames is null || AdditionalEventNames.Count == 0) return EventNames;
        return EventNames.Concat(AdditionalEventNames).Distinct(StringComparer.Ordinal).ToArray();
    }
    protected virtual IEnumerable<KeyValuePair<string, object?>> GetComponentParameters() => [];

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, TagName);
        foreach (var pair in GetAllParameters())
        {
            if (!ShouldRenderAttribute(pair.Value)) continue;
            var name = NormalizeAttributeName(pair.Key);
            if (pair.Value is bool b)
            {
                if (b) builder.AddAttribute(1, name, true);
            }
            else if (IsPrimitiveAttributeValue(pair.Value))
                builder.AddAttribute(1, name, Convert.ToString(pair.Value, CultureInfo.InvariantCulture));
            else
                builder.AddAttribute(1, name, JsonSerializer.Serialize(pair.Value, pair.Value!.GetType()));
        }
        builder.AddElementReferenceCapture(2, r => Element = r);
        BuildChildContent(builder);
        builder.CloseElement();
    }


    protected virtual void BuildChildContent(RenderTreeBuilder builder)
    {
        if (ChildContent is not null) builder.AddContent(3, ChildContent);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (_disposed) return;
        _module ??= await JS.InvokeAsync<IJSObjectReference>("import", GcdsInterop.ModulePath);
        _self ??= DotNetObjectReference.Create(this);
        // Render primitive values as attributes for useful SSR/initial markup, then also assign
        // every wrapper parameter as a web-component property after upgrade. Direct property
        // assignment is required for arrays/objects and keeps boolean/union props synchronized.
        var props = GetComponentParameters()
            .ToDictionary(p => NormalizePropertyName(p.Key), p => p.Value);
        if (Properties is not null)
            foreach (var p in Properties) props[NormalizePropertyName(p.Key)] = p.Value;

        // If a previously assigned property disappears on a later render, explicitly clear it.
        foreach (var removed in _configuredPropertyNames.Except(props.Keys).ToArray())
            props[removed] = null;

        _configuredPropertyNames = props.Where(p => p.Value is not null)
            .Select(p => p.Key).ToHashSet(StringComparer.Ordinal);
        await _module.InvokeVoidAsync("configure", Element, props, _self, GetEventNames());
    }

    private IEnumerable<KeyValuePair<string, object?>> GetAllParameters()
    {
        foreach (var p in GetComponentParameters()) yield return p;
        if (AdditionalAttributes is not null)
            foreach (var p in AdditionalAttributes) if (p.Value is not null) yield return p;
    }

    private static bool ShouldRenderAttribute(object? value) => value is not null;

    private static bool IsPrimitiveAttributeValue(object? value) => value is string or char or bool or byte or sbyte or short or ushort or int or uint or long or ulong or float or double or decimal or DateTime or DateTimeOffset or Guid or Enum;

    internal static string NormalizeAttributeName(string name)
    {
        if (name.Contains('-')) return name.ToLowerInvariant();
        var sb = new StringBuilder();
        for (var i = 0; i < name.Length; i++) {
            var c = name[i];
            if (char.IsUpper(c) && i > 0) sb.Append('-');
            sb.Append(char.ToLowerInvariant(c));
        }
        return sb.ToString();
    }

    internal static string NormalizePropertyName(string name)
    {
        if (!name.Contains('-')) return char.ToLowerInvariant(name[0]) + name[1..];
        var parts = name.Split('-', StringSplitOptions.RemoveEmptyEntries);
        return parts[0] + string.Concat(parts.Skip(1).Select(p => char.ToUpperInvariant(p[0]) + p[1..]));
    }

    [JSInvokable]
    public async Task HandleGcdsEventAsync(string name, JsonElement? detail)
    {
        var args = new GcdsEventArgs(name, detail);
        await OnEventCoreAsync(args);
        if (OnGcdsEvent.HasDelegate) await OnGcdsEvent.InvokeAsync(args);
        var callback = name switch {
            "gcdsDismiss" => OnGcdsDismiss, "gcdsClick" => OnGcdsClick, "gcdsFocus" => OnGcdsFocus,
            "gcdsBlur" => OnGcdsBlur, "gcdsInput" => OnGcdsInput, "gcdsChange" => OnGcdsChange,
            "gcdsError" => OnGcdsError, "gcdsValid" => OnGcdsValid, "gcdsSubmit" => OnGcdsSubmit,
            "gcdsRemoveFile" => OnGcdsRemoveFile, "gcdsSuggestionSelected" => OnGcdsSuggestionSelected,
            "gcdsTableStateChange" => OnGcdsTableStateChange, _ => default
        };
        if (callback.HasDelegate) await callback.InvokeAsync(args);
    }

    protected virtual Task OnEventCoreAsync(GcdsEventArgs args) => Task.CompletedTask;

    public async ValueTask<T> InvokeAsync<T>(string methodName, params object?[] args)
    {
        _module ??= await JS.InvokeAsync<IJSObjectReference>("import", GcdsInterop.ModulePath);
        return await _module.InvokeAsync<T>("invoke", Element, methodName, args);
    }
    public async ValueTask InvokeAsync(string methodName, params object?[] args)
    {
        _module ??= await JS.InvokeAsync<IJSObjectReference>("import", GcdsInterop.ModulePath);
        await _module.InvokeVoidAsync("invoke", Element, methodName, args);
    }
    public async ValueTask<T> GetPropertyAsync<T>(string propertyName)
    {
        _module ??= await JS.InvokeAsync<IJSObjectReference>("import", GcdsInterop.ModulePath);
        return await _module.InvokeAsync<T>("getProperty", Element, propertyName);
    }
    public async ValueTask SetPropertyAsync(string propertyName, object? value)
    {
        _module ??= await JS.InvokeAsync<IJSObjectReference>("import", GcdsInterop.ModulePath);
        await _module.InvokeVoidAsync("setProperty", Element, propertyName, value);
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed) return; _disposed = true;
        if (_module is not null) { try { await _module.InvokeVoidAsync("detach", Element); } catch (JSDisconnectedException) { } await _module.DisposeAsync(); }
        _self?.Dispose();
    }
}
