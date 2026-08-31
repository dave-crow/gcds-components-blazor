using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsAlert : GcdsComponentBase
{
    protected override string TagName => "gcds-alert";

    [Parameter] public string? AlertRole { get; set; }
    [Parameter] public string? Container { get; set; }
    [Parameter] public string? Heading { get; set; }
    [Parameter] public bool? HideCloseBtn { get; set; }
    [Parameter] public bool? HideRoleIcon { get; set; }
    [Parameter] public bool? IsFixed { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        if (AlertRole is not null) yield return new("alert-role", AlertRole);
        if (Container is not null) yield return new("container", Container);
        if (Heading is not null) yield return new("heading", Heading);
        if (HideCloseBtn is not null) yield return new("hide-close-btn", HideCloseBtn);
        if (HideRoleIcon is not null) yield return new("hide-role-icon", HideRoleIcon);
        if (IsFixed is not null) yield return new("is-fixed", IsFixed);
    }
}
