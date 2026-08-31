using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsGrid : GcdsComponentBase
{
    protected override string TagName => "gcds-grid";

    [Parameter] public string? AlignContent { get; set; }
    [Parameter] public string? AlignItems { get; set; }
    [Parameter] public string? Alignment { get; set; }
    [Parameter] public string? Columns { get; set; }
    [Parameter] public string? ColumnsDesktop { get; set; }
    [Parameter] public string? ColumnsTablet { get; set; }
    [Parameter] public string? Container { get; set; }
    [Parameter] public string? Display { get; set; }
    [Parameter] public bool? EqualRowHeight { get; set; }
    [Parameter] public string? Gap { get; set; }
    [Parameter] public string? GapDesktop { get; set; }
    [Parameter] public string? GapTablet { get; set; }
    [Parameter] public string? JustifyContent { get; set; }
    [Parameter] public string? JustifyItems { get; set; }
    [Parameter] public string? PlaceContent { get; set; }
    [Parameter] public string? PlaceItems { get; set; }
    [Parameter] public string? Tag { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        if (AlignContent is not null) yield return new("align-content", AlignContent);
        if (AlignItems is not null) yield return new("align-items", AlignItems);
        if (Alignment is not null) yield return new("alignment", Alignment);
        if (Columns is not null) yield return new("columns", Columns);
        if (ColumnsDesktop is not null) yield return new("columns-desktop", ColumnsDesktop);
        if (ColumnsTablet is not null) yield return new("columns-tablet", ColumnsTablet);
        if (Container is not null) yield return new("container", Container);
        if (Display is not null) yield return new("display", Display);
        if (EqualRowHeight is not null) yield return new("equal-row-height", EqualRowHeight);
        if (Gap is not null) yield return new("gap", Gap);
        if (GapDesktop is not null) yield return new("gap-desktop", GapDesktop);
        if (GapTablet is not null) yield return new("gap-tablet", GapTablet);
        if (JustifyContent is not null) yield return new("justify-content", JustifyContent);
        if (JustifyItems is not null) yield return new("justify-items", JustifyItems);
        if (PlaceContent is not null) yield return new("place-content", PlaceContent);
        if (PlaceItems is not null) yield return new("place-items", PlaceItems);
        if (Tag is not null) yield return new("tag", Tag);
    }
}
