using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsHeader : GcdsComponentBase
{
    protected override string TagName => "gcds-header";

    [Parameter] public string? LangHref { get; set; }
    [Parameter] public bool? SignatureHasLink { get; set; }
    [Parameter] public string? SkipToHref { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        if (LangHref is not null) yield return new("lang-href", LangHref);
        if (SignatureHasLink is not null) yield return new("signature-has-link", SignatureHasLink);
        if (SkipToHref is not null) yield return new("skip-to-href", SkipToHref);
    }
}
