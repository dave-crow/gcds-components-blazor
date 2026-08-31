using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsStepper : GcdsComponentBase
{
    protected override string TagName => "gcds-stepper";

    [Parameter] public int? CurrentStep { get; set; }
    [Parameter] public string? Tag { get; set; }
    [Parameter] public int? TotalSteps { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        if (CurrentStep is not null) yield return new("current-step", CurrentStep);
        if (Tag is not null) yield return new("tag", Tag);
        if (TotalSteps is not null) yield return new("total-steps", TotalSteps);
    }
}
