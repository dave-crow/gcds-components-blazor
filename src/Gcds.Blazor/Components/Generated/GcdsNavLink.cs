using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsNavLink : GcdsComponentBase
{
    protected override string TagName => "gcds-nav-link";

    [Parameter] public bool? Current { get; set; }
    [Parameter] public string? Href { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        if (Current is not null) yield return new("current", Current);
        if (Href is not null) yield return new("href", Href);
    }

public ValueTask FocusLinkAsync() => InvokeAsync("focusLink");
}
