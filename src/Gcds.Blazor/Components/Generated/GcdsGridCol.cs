using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsGridCol : GcdsComponentBase
{
    protected override string TagName => "gcds-grid-col";

    [Parameter] public int? Desktop { get; set; }
    [Parameter] public int? Tablet { get; set; }
    [Parameter] public string? Tag { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        if (Desktop is not null) yield return new("desktop", Desktop);
        if (Tablet is not null) yield return new("tablet", Tablet);
        if (Tag is not null) yield return new("tag", Tag);
    }
}
