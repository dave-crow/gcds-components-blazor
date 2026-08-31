using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsNavGroup : GcdsComponentBase
{
    protected override string TagName => "gcds-nav-group";

    [Parameter] public string? CloseTrigger { get; set; }
    [Parameter] public string? MenuLabel { get; set; }
    [Parameter] public bool? Open { get; set; }
    [Parameter] public string? OpenTrigger { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        if (CloseTrigger is not null) yield return new("close-trigger", CloseTrigger);
        if (MenuLabel is not null) yield return new("menu-label", MenuLabel);
        if (Open is not null) yield return new("open", Open);
        if (OpenTrigger is not null) yield return new("open-trigger", OpenTrigger);
    }

public ValueTask FocusTriggerAsync() => InvokeAsync("focusTrigger");
    public ValueTask ToggleNavAsync() => InvokeAsync("toggleNav");
}
