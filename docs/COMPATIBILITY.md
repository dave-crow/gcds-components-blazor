# Compatibility baseline

This implementation targets the released **`@gcds-core/components` 1.4.0** package/tag.

GCDS's Stencil configuration generates React, Angular, and Vue framework outputs from the same web-component source. The Blazor wrapper catalog therefore mirrors the full released Stencil component type surface: **41 components**.

The React 1.4.0 package also replaces its generated table export with a custom `GcdsTableWithSlots` wrapper that manages dynamic table-cell slots. `Gcds.Blazor.GcdsTable` contains an equivalent Blazor-specific managed-slot layer through `GcdsTableColumn.RenderCell` while still delegating actual table behavior to `<gcds-table>`.

## Validation performed while generating this package

- Confirmed the published npm package is 1.4.0.
- Reviewed the 1.4.0 generated `components.d.ts` API surface rather than generating against unreleased `main` changes.
- Reviewed the Stencil output-target configuration for React, Angular, and Vue.
- Added **41 wrapper classes** and ran an automated catalog check: **41/41**.
- Reviewed the generated DOM event maps and registered **12/12** released custom event names:
  - `gcdsDismiss`
  - `gcdsClick`
  - `gcdsFocus`
  - `gcdsBlur`
  - `gcdsInput`
  - `gcdsChange`
  - `gcdsError`
  - `gcdsValid`
  - `gcdsSubmit`
  - `gcdsRemoveFile`
  - `gcdsSuggestionSelected`
  - `gcdsTableStateChange`
- Tested JavaScript property assignment, event forwarding, listener cleanup, web-component method invocation, and property get/set using Node's test runner.
- Added an ASP.NET Core Blazor Web App sample and an all-components Razor smoke page.
- Added GitHub Actions CI that installs GCDS, executes the JS/coverage tests, and runs `dotnet restore` + `dotnet build`.

## Environment limitation

The artifact-generation environment has Node.js but does **not** have the .NET SDK installed and cannot resolve the npm registry from the local container. Consequently, `dotnet restore`, `dotnet build`, bUnit, and a real browser/Playwright run against the sample could not be executed here.

Run `build.ps1` on a machine with the .NET 8 SDK and npm access. After that succeeds, set `Gcds.Blazor.Demo` as the Visual Studio startup project and exercise the smoke pages in a browser.

## Deliberate limitations

### Browser file streaming

The file-uploader wrapper exposes GCDS's serializable `string[]` value, events, properties, and validation methods. Browser `File` objects / `FileList` cannot simply be JSON-serialized into .NET file streams, so this version does not claim `IBrowserFile` parity with Blazor `InputFile`.

### JavaScript function-valued validators

GCDS allows JavaScript function validators. Normal C# delegates cannot be JSON-serialized into executable browser functions. The wrapper exposes the validator property and generic property interop so an advanced application can bridge an `IJSObjectReference`, but a dedicated typed custom-validator abstraction is not included yet.

## Forward compatibility

The wrapper provides four fallback mechanisms for additive upstream changes:

1. `AdditionalAttributes` for primitive attributes.
2. `Properties` / `SetPropertyAsync` for JavaScript properties.
3. `InvokeAsync` / `GetPropertyAsync` for new methods and read-only properties.
4. `AdditionalEventNames` plus `OnGcdsEvent` for new custom events.

These mechanisms reduce the need for immediate wrapper releases when GCDS adds non-breaking features, while the pinned dependency keeps production upgrades deliberate.
