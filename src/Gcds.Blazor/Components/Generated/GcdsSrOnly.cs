using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsSrOnly : GcdsComponentBase
{
    protected override string TagName => "gcds-sr-only";

    [Parameter] public string? Tag { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        if (Tag is not null) yield return new("tag", Tag);
    }
}
