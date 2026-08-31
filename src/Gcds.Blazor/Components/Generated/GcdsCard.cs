using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsCard : GcdsComponentBase
{
    protected override string TagName => "gcds-card";

    [Parameter] public string? Badge { get; set; }
    [Parameter] public string? CardTitle { get; set; }
    [Parameter] public string? CardTitleTag { get; set; }
    [Parameter] public string? Description { get; set; }
    [Parameter] public string? Href { get; set; }
    [Parameter] public string? ImgAlt { get; set; }
    [Parameter] public string? ImgSrc { get; set; }
    [Parameter] public string? Rel { get; set; }
    [Parameter] public string? Target { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        if (Badge is not null) yield return new("badge", Badge);
        if (CardTitle is not null) yield return new("card-title", CardTitle);
        if (CardTitleTag is not null) yield return new("card-title-tag", CardTitleTag);
        if (Description is not null) yield return new("description", Description);
        if (Href is not null) yield return new("href", Href);
        if (ImgAlt is not null) yield return new("img-alt", ImgAlt);
        if (ImgSrc is not null) yield return new("img-src", ImgSrc);
        if (Rel is not null) yield return new("rel", Rel);
        if (Target is not null) yield return new("target", Target);
    }
}
