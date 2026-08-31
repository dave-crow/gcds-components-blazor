using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsContainer : GcdsComponentBase
{
    protected override string TagName => "gcds-container";

    [Parameter] public string? Alignment { get; set; }
    [Parameter] public bool? Border { get; set; }
    [Parameter] public string? Layout { get; set; }
    [Parameter] public string? Margin { get; set; }
    [Parameter] public string? Padding { get; set; }
    [Parameter] public string? Size { get; set; }
    [Parameter] public string? Tag { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        if (Alignment is not null) yield return new("alignment", Alignment);
        if (Border is not null) yield return new("border", Border);
        if (Layout is not null) yield return new("layout", Layout);
        if (Margin is not null) yield return new("margin", Margin);
        if (Padding is not null) yield return new("padding", Padding);
        if (Size is not null) yield return new("size", Size);
        if (Tag is not null) yield return new("tag", Tag);
    }
}
