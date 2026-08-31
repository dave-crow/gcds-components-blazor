using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsLabel : GcdsComponentBase
{
    protected override string TagName => "gcds-label";

    [Parameter] public bool? HideLabel { get; set; }
    [Parameter] public string? Label { get; set; }
    [Parameter] public string? LabelFor { get; set; }
    [Parameter] public bool? Required { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        if (HideLabel is not null) yield return new("hide-label", HideLabel);
        if (Label is not null) yield return new("label", Label);
        if (LabelFor is not null) yield return new("label-for", LabelFor);
        if (Required is not null) yield return new("required", Required);
    }
}
