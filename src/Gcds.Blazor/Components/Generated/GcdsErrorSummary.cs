using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsErrorSummary : GcdsComponentBase
{
    protected override string TagName => "gcds-error-summary";

    [Parameter] public object? ErrorLinks { get; set; }
    [Parameter] public string? Heading { get; set; }
    [Parameter] public bool? Listen { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        if (ErrorLinks is not null) yield return new("error-links", ErrorLinks);
        if (Heading is not null) yield return new("heading", Heading);
        if (Listen is not null) yield return new("listen", Listen);
    }
}
