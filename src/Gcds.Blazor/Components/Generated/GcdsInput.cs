using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsInput : Gcds.Blazor.Forms.GcdsBindableStringComponent
{
    protected override string TagName => "gcds-input";

    [Parameter] public string? Autocomplete { get; set; }
    [Parameter] public bool? Autofocus { get; set; }
    [Parameter] public bool? Disabled { get; set; }
    [Parameter] public string? ErrorMessage { get; set; }
    [Parameter] public string? Form { get; set; }
    [Parameter] public bool? HideLabel { get; set; }
    [Parameter] public string? Hint { get; set; }
    [Parameter] public string? InputId { get; set; }
    [Parameter] public string? Inputmode { get; set; }
    [Parameter] public string? Label { get; set; }
    [Parameter] public object? Max { get; set; }
    [Parameter] public int? Maxlength { get; set; }
    [Parameter] public object? Min { get; set; }
    [Parameter] public int? Minlength { get; set; }
    [Parameter] public string? Name { get; set; }
    [Parameter] public string? Pattern { get; set; }
    [Parameter] public bool? Readonly { get; set; }
    [Parameter] public bool? Required { get; set; }
    [Parameter] public int? Size { get; set; }
    [Parameter] public object? Step { get; set; }
    [Parameter] public object? Suggestions { get; set; }
    [Parameter] public string? Type { get; set; }
    [Parameter] public string? ValidateOn { get; set; }
    [Parameter] public object? Validator { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        foreach (var p in GetBindingParameter()) yield return p;
        if (Autocomplete is not null) yield return new("autocomplete", Autocomplete);
        if (Autofocus is not null) yield return new("autofocus", Autofocus);
        if (Disabled is not null) yield return new("disabled", Disabled);
        if (ErrorMessage is not null) yield return new("error-message", ErrorMessage);
        if (Form is not null) yield return new("form", Form);
        if (HideLabel is not null) yield return new("hide-label", HideLabel);
        if (Hint is not null) yield return new("hint", Hint);
        if (InputId is not null) yield return new("input-id", InputId);
        if (Inputmode is not null) yield return new("inputmode", Inputmode);
        if (Label is not null) yield return new("label", Label);
        if (Max is not null) yield return new("max", Max);
        if (Maxlength is not null) yield return new("maxlength", Maxlength);
        if (Min is not null) yield return new("min", Min);
        if (Minlength is not null) yield return new("minlength", Minlength);
        if (Name is not null) yield return new("name", Name);
        if (Pattern is not null) yield return new("pattern", Pattern);
        if (Readonly is not null) yield return new("readonly", Readonly);
        if (Required is not null) yield return new("required", Required);
        if (Size is not null) yield return new("size", Size);
        if (Step is not null) yield return new("step", Step);
        if (Suggestions is not null) yield return new("suggestions", Suggestions);
        if (Type is not null) yield return new("type", Type);
        if (ValidateOn is not null) yield return new("validate-on", ValidateOn);
        if (Validator is not null) yield return new("validator", Validator);
    }

public ValueTask<bool> CheckValidityAsync() => InvokeAsync<bool>("checkValidity");
    public ValueTask ValidateAsync() => InvokeAsync("validate");
    public ValueTask<string> GetValidationMessageAsync() => InvokeAsync<string>("getValidationMessage");
}
