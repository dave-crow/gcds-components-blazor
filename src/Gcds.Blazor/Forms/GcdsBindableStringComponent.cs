using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Gcds.Blazor.Components;

namespace Gcds.Blazor.Forms;

public abstract class GcdsBindableStringComponent : GcdsComponentBase
{
    [Parameter] public string? Value { get; set; }
    [Parameter] public EventCallback<string?> ValueChanged { get; set; }
    [Parameter] public Expression<Func<string?>>? ValueExpression { get; set; }
    [CascadingParameter] private EditContext? EditContext { get; set; }
    private FieldIdentifier? _field;

    protected IEnumerable<KeyValuePair<string, object?>> GetBindingParameter() { if (Value is not null) yield return new("value", Value); }
    protected override async Task OnEventCoreAsync(GcdsEventArgs args)
    {
        if (args.Name is not ("gcdsInput" or "gcdsChange")) return;
        var value = args.GetDetail<string>();
        if (value == Value) return;
        Value = value;
        await ValueChanged.InvokeAsync(value);
        if (EditContext is not null && ValueExpression is not null) {
            _field ??= FieldIdentifier.Create(ValueExpression);
            EditContext.NotifyFieldChanged(_field.Value);
        }
    }
}
