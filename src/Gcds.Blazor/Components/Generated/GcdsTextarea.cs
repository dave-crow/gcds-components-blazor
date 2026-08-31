using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsTextarea : Gcds.Blazor.Forms.GcdsBindableStringComponent
{
    protected override string TagName => "gcds-textarea";

    [Parameter] public bool? Autofocus { get; set; }
    [Parameter] public int? Cols { get; set; }
    [Parameter] public bool? Disabled { get; set; }
    [Parameter] public string? ErrorMessage { get; set; }
    [Parameter] public string? Form { get; set; }
    [Parameter] public bool? HideLabel { get; set; }
    [Parameter] public bool? HideLimit { get; set; }
    [Parameter] public string? Hint { get; set; }
    [Parameter] public string? Label { get; set; }
    [Parameter] public int? Maxlength { get; set; }
    [Parameter] public int? Minlength { get; set; }
    [Parameter] public string? Name { get; set; }
    [Parameter] public bool? Required { get; set; }
    [Parameter] public int? Rows { get; set; }
    [Parameter] public string? TextareaId { get; set; }
    [Parameter] public string? ValidateOn { get; set; }
    [Parameter] public object? Validator { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        foreach (var p in GetBindingParameter()) yield return p;
        if (Autofocus is not null) yield return new("autofocus", Autofocus);
        if (Cols is not null) yield return new("cols", Cols);
        if (Disabled is not null) yield return new("disabled", Disabled);
        if (ErrorMessage is not null) yield return new("error-message", ErrorMessage);
        if (Form is not null) yield return new("form", Form);
        if (HideLabel is not null) yield return new("hide-label", HideLabel);
        if (HideLimit is not null) yield return new("hide-limit", HideLimit);
        if (Hint is not null) yield return new("hint", Hint);
        if (Label is not null) yield return new("label", Label);
        if (Maxlength is not null) yield return new("maxlength", Maxlength);
        if (Minlength is not null) yield return new("minlength", Minlength);
        if (Name is not null) yield return new("name", Name);
        if (Required is not null) yield return new("required", Required);
        if (Rows is not null) yield return new("rows", Rows);
        if (TextareaId is not null) yield return new("textarea-id", TextareaId);
        if (ValidateOn is not null) yield return new("validate-on", ValidateOn);
        if (Validator is not null) yield return new("validator", Validator);
    }

public ValueTask<bool> CheckValidityAsync() => InvokeAsync<bool>("checkValidity");
    public ValueTask ValidateAsync() => InvokeAsync("validate");
    public ValueTask<string> GetValidationMessageAsync() => InvokeAsync<string>("getValidationMessage");
}
