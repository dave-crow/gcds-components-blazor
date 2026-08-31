using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsTopNav : GcdsComponentBase
{
    protected override string TagName => "gcds-top-nav";

    [Parameter] public string? Alignment { get; set; }
    [Parameter] public string? Label { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        if (Alignment is not null) yield return new("alignment", Alignment);
        if (Label is not null) yield return new("label", Label);
    }

public ValueTask<string> GetNavSizeAsync() => InvokeAsync<string>("getNavSize");
    public ValueTask UpdateNavItemQueueAsync(object element, bool includeElement = false) => InvokeAsync("updateNavItemQueue", element, includeElement);
    public ValueTask UpdateNavSizeAsync(object size) => InvokeAsync("updateNavSize", size);
}
