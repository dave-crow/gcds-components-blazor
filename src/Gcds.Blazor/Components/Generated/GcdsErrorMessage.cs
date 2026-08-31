using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsErrorMessage : GcdsComponentBase
{
    protected override string TagName => "gcds-error-message";

    [Parameter] public string? MessageId { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        if (MessageId is not null) yield return new("message-id", MessageId);
    }
}
