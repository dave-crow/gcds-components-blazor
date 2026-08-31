using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsLink : GcdsComponentBase
{
    protected override string TagName => "gcds-link";

    [Parameter] public string? Display { get; set; }
    [Parameter] public string? Download { get; set; }
    [Parameter] public bool? External { get; set; }
    [Parameter] public string? Href { get; set; }
    [Parameter] public string? LinkRole { get; set; }
    [Parameter] public string? Rel { get; set; }
    [Parameter] public string? Size { get; set; }
    [Parameter] public string? Target { get; set; }
    [Parameter] public string? Type { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        if (Display is not null) yield return new("display", Display);
        if (Download is not null) yield return new("download", Download);
        if (External is not null) yield return new("external", External);
        if (Href is not null) yield return new("href", Href);
        if (LinkRole is not null) yield return new("link-role", LinkRole);
        if (Rel is not null) yield return new("rel", Rel);
        if (Size is not null) yield return new("size", Size);
        if (Target is not null) yield return new("target", Target);
        if (Type is not null) yield return new("type", Type);
    }
}
