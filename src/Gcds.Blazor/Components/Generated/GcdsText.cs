using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsText : GcdsComponentBase
{
    protected override string TagName => "gcds-text";

    [Parameter] public bool? CharacterLimit { get; set; }
    [Parameter] public string? Display { get; set; }
    [Parameter] public string? MarginBottom { get; set; }
    [Parameter] public string? MarginTop { get; set; }
    [Parameter] public string? Size { get; set; }
    [Parameter] public string? TextRole { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        if (CharacterLimit is not null) yield return new("character-limit", CharacterLimit);
        if (Display is not null) yield return new("display", Display);
        if (MarginBottom is not null) yield return new("margin-bottom", MarginBottom);
        if (MarginTop is not null) yield return new("margin-top", MarginTop);
        if (Size is not null) yield return new("size", Size);
        if (TextRole is not null) yield return new("text-role", TextRole);
    }
}
