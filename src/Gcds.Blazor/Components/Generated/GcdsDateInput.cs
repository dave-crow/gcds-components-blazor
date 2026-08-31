using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsDateInput : Gcds.Blazor.Forms.GcdsBindableStringComponent
{
    protected override string TagName => "gcds-date-input";

    [Parameter] public bool? Autofocus { get; set; }
    [Parameter] public bool? Disabled { get; set; }
    [Parameter] public string? ErrorMessage { get; set; }
    [Parameter] public string? Form { get; set; }
    [Parameter] public string? Format { get; set; }
    [Parameter] public string? Hint { get; set; }
    [Parameter] public string? Legend { get; set; }
    [Parameter] public string? Max { get; set; }
    [Parameter] public string? Min { get; set; }
    [Parameter] public string? Name { get; set; }
    [Parameter] public bool? Required { get; set; }
    [Parameter] public string? ValidateOn { get; set; }
    [Parameter] public object? Validator { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        foreach (var p in GetBindingParameter()) yield return p;
        if (Autofocus is not null) yield return new("autofocus", Autofocus);
        if (Disabled is not null) yield return new("disabled", Disabled);
        if (ErrorMessage is not null) yield return new("error-message", ErrorMessage);
        if (Form is not null) yield return new("form", Form);
        if (Format is not null) yield return new("format", Format);
        if (Hint is not null) yield return new("hint", Hint);
        if (Legend is not null) yield return new("legend", Legend);
        if (Max is not null) yield return new("max", Max);
        if (Min is not null) yield return new("min", Min);
        if (Name is not null) yield return new("name", Name);
        if (Required is not null) yield return new("required", Required);
        if (ValidateOn is not null) yield return new("validate-on", ValidateOn);
        if (Validator is not null) yield return new("validator", Validator);
    }

public ValueTask<bool> CheckValidityAsync() => InvokeAsync<bool>("checkValidity");
    public ValueTask ValidateAsync() => InvokeAsync("validate");
    public ValueTask<string> GetValidationMessageAsync() => InvokeAsync<string>("getValidationMessage");

    // Events from the date input's three nested controls bubble with only the value of
    // the control that changed. Read the combined YYYY-MM-DD host value instead.
    protected override async ValueTask<string?> GetEventValueAsync(GcdsEventArgs args) =>
        await GetPropertyAsync<string?>("value");
}
