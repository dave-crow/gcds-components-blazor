using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsCheckboxes : Gcds.Blazor.Forms.GcdsBindableStringListComponent
{
    protected override string TagName => "gcds-checkboxes";

    [Parameter] public bool? Autofocus { get; set; }
    [Parameter] public bool? Disabled { get; set; }
    [Parameter] public string? ErrorMessage { get; set; }
    [Parameter] public string? Form { get; set; }
    [Parameter] public bool? HideLabel { get; set; }
    [Parameter] public bool? HideLegend { get; set; }
    [Parameter] public string? Hint { get; set; }
    [Parameter] public string? Legend { get; set; }
    [Parameter] public string? Name { get; set; }
    [Parameter] public object? Options { get; set; }
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
        if (HideLabel is not null) yield return new("hide-label", HideLabel);
        if (HideLegend is not null) yield return new("hide-legend", HideLegend);
        if (Hint is not null) yield return new("hint", Hint);
        if (Legend is not null) yield return new("legend", Legend);
        if (Name is not null) yield return new("name", Name);
        if (Options is not null) yield return new("options", Options);
        if (Required is not null) yield return new("required", Required);
        if (ValidateOn is not null) yield return new("validate-on", ValidateOn);
        if (Validator is not null) yield return new("validator", Validator);
    }

public ValueTask<bool> CheckValidityAsync() => InvokeAsync<bool>("checkValidity");
    public ValueTask ValidateAsync() => InvokeAsync("validate");
    public ValueTask<string> GetValidationMessageAsync() => InvokeAsync<string>("getValidationMessage");
}
