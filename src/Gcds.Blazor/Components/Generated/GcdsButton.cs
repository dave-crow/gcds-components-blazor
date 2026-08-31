using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsButton : GcdsComponentBase
{
    protected override string TagName => "gcds-button";

    [Parameter] public string? ButtonId { get; set; }
    [Parameter] public string? ButtonRole { get; set; }
    [Parameter] public bool? Disabled { get; set; }
    [Parameter] public string? Download { get; set; }
    [Parameter] public string? Href { get; set; }
    [Parameter] public string? Name { get; set; }
    [Parameter] public string? Rel { get; set; }
    [Parameter] public string? Size { get; set; }
    [Parameter] public string? Target { get; set; }
    [Parameter] public string? Type { get; set; }
    [Parameter] public string? Value { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        if (ButtonId is not null) yield return new("button-id", ButtonId);
        if (ButtonRole is not null) yield return new("button-role", ButtonRole);
        if (Disabled is not null) yield return new("disabled", Disabled);
        if (Download is not null) yield return new("download", Download);
        if (Href is not null) yield return new("href", Href);
        if (Name is not null) yield return new("name", Name);
        if (Rel is not null) yield return new("rel", Rel);
        if (Size is not null) yield return new("size", Size);
        if (Target is not null) yield return new("target", Target);
        if (Type is not null) yield return new("type", Type);
        if (Value is not null) yield return new("value", Value);
    }
}
