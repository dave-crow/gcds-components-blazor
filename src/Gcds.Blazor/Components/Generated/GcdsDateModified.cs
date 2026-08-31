using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsDateModified : GcdsComponentBase
{
    protected override string TagName => "gcds-date-modified";

    [Parameter] public string? Type { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        if (Type is not null) yield return new("type", Type);
    }
}
