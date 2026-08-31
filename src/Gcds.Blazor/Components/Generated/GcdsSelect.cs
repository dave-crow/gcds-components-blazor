using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsSelect : Gcds.Blazor.Forms.GcdsBindableStringComponent
{
    protected override string TagName => "gcds-select";

    [Parameter] public string? Autocomplete { get; set; }
    [Parameter] public bool? Autofocus { get; set; }
    [Parameter] public string? DefaultValue { get; set; }
    [Parameter] public bool? Disabled { get; set; }
    [Parameter] public string? ErrorMessage { get; set; }
    [Parameter] public string? Form { get; set; }
    [Parameter] public bool? HideLabel { get; set; }
    [Parameter] public string? Hint { get; set; }
    [Parameter] public string? Label { get; set; }
    [Parameter] public string? Name { get; set; }
    [Parameter] public bool? Required { get; set; }
    [Parameter] public string? SelectId { get; set; }
    [Parameter] public string? ValidateOn { get; set; }
    [Parameter] public object? Validator { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        foreach (var p in GetBindingParameter()) yield return p;
        if (Autocomplete is not null) yield return new("autocomplete", Autocomplete);
        if (Autofocus is not null) yield return new("autofocus", Autofocus);
        if (DefaultValue is not null) yield return new("default-value", DefaultValue);
        if (Disabled is not null) yield return new("disabled", Disabled);
        if (ErrorMessage is not null) yield return new("error-message", ErrorMessage);
        if (Form is not null) yield return new("form", Form);
        if (HideLabel is not null) yield return new("hide-label", HideLabel);
        if (Hint is not null) yield return new("hint", Hint);
        if (Label is not null) yield return new("label", Label);
        if (Name is not null) yield return new("name", Name);
        if (Required is not null) yield return new("required", Required);
        if (SelectId is not null) yield return new("select-id", SelectId);
        if (ValidateOn is not null) yield return new("validate-on", ValidateOn);
        if (Validator is not null) yield return new("validator", Validator);
    }

public ValueTask<bool> CheckValidityAsync() => InvokeAsync<bool>("checkValidity");
    public ValueTask ValidateAsync() => InvokeAsync("validate");
    public ValueTask<string> GetValidationMessageAsync() => InvokeAsync<string>("getValidationMessage");
}
