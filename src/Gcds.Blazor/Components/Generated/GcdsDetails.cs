using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsDetails : GcdsComponentBase
{
    protected override string TagName => "gcds-details";

    [Parameter] public string? DetailsTitle { get; set; }
    [Parameter] public bool? Open { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        if (DetailsTitle is not null) yield return new("details-title", DetailsTitle);
        if (Open is not null) yield return new("open", Open);
    }

public ValueTask ToggleAsync() => InvokeAsync("toggle");
}
