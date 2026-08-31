using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsTopicMenu : GcdsComponentBase
{
    protected override string TagName => "gcds-topic-menu";

    [Parameter] public bool? Home { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        if (Home is not null) yield return new("home", Home);
    }

public ValueTask CloseAllMenusAsync() => InvokeAsync("closeAllMenus");
    public ValueTask<string> GetNavSizeAsync() => InvokeAsync<string>("getNavSize");
    public ValueTask ToggleNavAsync() => InvokeAsync("toggleNav");
    public ValueTask UpdateNavItemQueueAsync(object parent) => InvokeAsync("updateNavItemQueue", parent);
    public ValueTask UpdateNavSizeAsync(object size) => InvokeAsync("updateNavSize", size);
}
