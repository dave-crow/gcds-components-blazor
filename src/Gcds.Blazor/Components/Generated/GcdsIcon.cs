using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsIcon : GcdsComponentBase
{
    protected override string TagName => "gcds-icon";

    [Parameter] public string? Label { get; set; }
    [Parameter] public string? MarginLeft { get; set; }
    [Parameter] public string? MarginRight { get; set; }
    [Parameter] public string? Name { get; set; }
    [Parameter] public string? Size { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        if (Label is not null) yield return new("label", Label);
        if (MarginLeft is not null) yield return new("margin-left", MarginLeft);
        if (MarginRight is not null) yield return new("margin-right", MarginRight);
        if (Name is not null) yield return new("name", Name);
        if (Size is not null) yield return new("size", Size);
    }
}
