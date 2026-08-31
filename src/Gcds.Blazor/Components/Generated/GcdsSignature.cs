using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsSignature : GcdsComponentBase
{
    protected override string TagName => "gcds-signature";

    [Parameter] public bool? HasLink { get; set; }
    [Parameter] public string? Type { get; set; }
    [Parameter] public string? Variant { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        if (HasLink is not null) yield return new("has-link", HasLink);
        if (Type is not null) yield return new("type", Type);
        if (Variant is not null) yield return new("variant", Variant);
    }
}
