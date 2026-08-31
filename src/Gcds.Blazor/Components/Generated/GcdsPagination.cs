using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsPagination : GcdsComponentBase
{
    protected override string TagName => "gcds-pagination";

    [Parameter] public int? CurrentPage { get; set; }
    [Parameter] public string? Display { get; set; }
    [Parameter] public string? Label { get; set; }
    [Parameter] public string? NextHref { get; set; }
    [Parameter] public string? NextLabel { get; set; }
    [Parameter] public string? PreviousHref { get; set; }
    [Parameter] public string? PreviousLabel { get; set; }
    [Parameter] public int? TotalPages { get; set; }
    [Parameter] public object? Url { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        if (CurrentPage is not null) yield return new("current-page", CurrentPage);
        if (Display is not null) yield return new("display", Display);
        if (Label is not null) yield return new("label", Label);
        if (NextHref is not null) yield return new("next-href", NextHref);
        if (NextLabel is not null) yield return new("next-label", NextLabel);
        if (PreviousHref is not null) yield return new("previous-href", PreviousHref);
        if (PreviousLabel is not null) yield return new("previous-label", PreviousLabel);
        if (TotalPages is not null) yield return new("total-pages", TotalPages);
        if (Url is not null) yield return new("url", Url);
    }
}
