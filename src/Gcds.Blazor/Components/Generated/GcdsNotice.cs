using Microsoft.AspNetCore.Components;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsNotice : GcdsComponentBase
{
    protected override string TagName => "gcds-notice";

    [Parameter] public string? NoticeRole { get; set; }
    [Parameter] public string? NoticeTitle { get; set; }
    [Parameter] public string? NoticeTitleTag { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        if (NoticeRole is not null) yield return new("notice-role", NoticeRole);
        if (NoticeTitle is not null) yield return new("notice-title", NoticeTitle);
        if (NoticeTitleTag is not null) yield return new("notice-title-tag", NoticeTitleTag);
    }
}
