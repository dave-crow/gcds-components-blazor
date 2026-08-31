namespace Gcds.Blazor.Interop;

public static class GcdsInterop
{
    public const string ModulePath = "./_content/Gcds.Blazor/js/gcds-blazor.js";

    // Complete custom-event surface published by @gcds-core/components 1.4.0.
    public static IReadOnlyCollection<string> DefaultEvents { get; } = new[]
    {
        "gcdsDismiss",
        "gcdsClick",
        "gcdsFocus",
        "gcdsBlur",
        "gcdsInput",
        "gcdsChange",
        "gcdsError",
        "gcdsValid",
        "gcdsSubmit",
        "gcdsRemoveFile",
        "gcdsSuggestionSelected",
        "gcdsTableStateChange"
    };
}
