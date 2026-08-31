using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsHeading : GcdsComponentBase
{
    protected override string TagName => "gcds-heading";

    [Parameter] public bool? CharacterLimit { get; set; }
    [Parameter] public string? HeadingRole { get; set; }
    [Parameter] public string? MarginBottom { get; set; }
    [Parameter] public string? MarginTop { get; set; }
    [Parameter] public string? Tag { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        if (CharacterLimit is not null) yield return new("character-limit", CharacterLimit);
        if (HeadingRole is not null) yield return new("heading-role", HeadingRole);
        if (MarginBottom is not null) yield return new("margin-bottom", MarginBottom);
        if (MarginTop is not null) yield return new("margin-top", MarginTop);
        if (Tag is not null) yield return new("tag", Tag);
    }
}
