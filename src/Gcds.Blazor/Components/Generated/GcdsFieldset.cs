using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsFieldset : GcdsComponentBase
{
    protected override string TagName => "gcds-fieldset";

    [Parameter] public string? Hint { get; set; }
    [Parameter] public string? Legend { get; set; }
    [Parameter] public string? LegendSize { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        if (Hint is not null) yield return new("hint", Hint);
        if (Legend is not null) yield return new("legend", Legend);
        if (LegendSize is not null) yield return new("legend-size", LegendSize);
    }
}
