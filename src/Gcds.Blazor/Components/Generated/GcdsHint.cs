using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsHint : GcdsComponentBase
{
    protected override string TagName => "gcds-hint";

    [Parameter] public string? HintId { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        if (HintId is not null) yield return new("hint-id", HintId);
    }
}
