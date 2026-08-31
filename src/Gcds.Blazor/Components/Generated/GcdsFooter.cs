using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsFooter : GcdsComponentBase
{
    protected override string TagName => "gcds-footer";

    [Parameter] public string? ContextualHeading { get; set; }
    [Parameter] public object? ContextualLinks { get; set; }
    [Parameter] public string? Display { get; set; }
    [Parameter] public object? SubLinks { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        if (ContextualHeading is not null) yield return new("contextual-heading", ContextualHeading);
        if (ContextualLinks is not null) yield return new("contextual-links", ContextualLinks);
        if (Display is not null) yield return new("display", Display);
        if (SubLinks is not null) yield return new("sub-links", SubLinks);
    }
}
