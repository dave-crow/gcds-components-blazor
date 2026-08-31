using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsLangToggle : GcdsComponentBase
{
    protected override string TagName => "gcds-lang-toggle";

    [Parameter] public string? Href { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        if (Href is not null) yield return new("href", Href);
    }
}
