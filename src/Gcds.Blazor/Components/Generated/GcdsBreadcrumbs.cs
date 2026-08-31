using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsBreadcrumbs : GcdsComponentBase
{
    protected override string TagName => "gcds-breadcrumbs";

    [Parameter] public bool? HideCanadaLink { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        if (HideCanadaLink is not null) yield return new("hide-canada-link", HideCanadaLink);
    }
}
