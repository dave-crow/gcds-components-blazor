using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsSearch : Gcds.Blazor.Forms.GcdsBindableStringComponent
{
    protected override string TagName => "gcds-search";

    [Parameter] public string? Action { get; set; }
    [Parameter] public string? Method { get; set; }
    [Parameter] public string? Name { get; set; }
    [Parameter] public string? Placeholder { get; set; }
    [Parameter] public string? SearchId { get; set; }
    [Parameter] public object? Suggested { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        foreach (var p in GetBindingParameter()) yield return p;
        if (Action is not null) yield return new("action", Action);
        if (Method is not null) yield return new("method", Method);
        if (Name is not null) yield return new("name", Name);
        if (Placeholder is not null) yield return new("placeholder", Placeholder);
        if (SearchId is not null) yield return new("search-id", SearchId);
        if (Suggested is not null) yield return new("suggested", Suggested);
    }
}
