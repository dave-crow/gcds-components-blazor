# Gcds.Blazor

A Blazor Razor Class Library that wraps the official **GC Design System (GCDS)** web components and exposes them as reusable C# / Razor components for ASP.NET Core Blazor applications.

This implementation is pinned to **`@gcds-core/components` 1.4.0**. It intentionally wraps the official web components rather than reimplementing their HTML, CSS, accessibility logic, or design tokens.

## What is included

- **41/41** Blazor wrappers for the released GCDS 1.4.0 component surface.
- **12/12** released GCDS custom event names bridged to `EventCallback<GcdsEventArgs>`.
- Typed C# parameters for normal component properties.
- `AdditionalAttributes` for normal HTML / ARIA / data attributes and forward-compatible primitive GCDS attributes.
- `Properties` plus `SetPropertyAsync` / `GetPropertyAsync` for JavaScript-only or future web-component properties.
- Automatic property assignment for arrays and objects such as checkbox options and table data.
- `@bind-Value` support for Input, Textarea, Select, Date Input, Radios, Checkboxes, Search, and the File Uploader's serializable `string[]` value.
- `EditContext.NotifyFieldChanged` integration so bound GCDS form controls participate in Blazor edit state.
- Typed wrappers for public GCDS methods such as `CheckValidityAsync`, `ValidateAsync`, `ToggleAsync`, and `GetVisibleRowsAsync`.
- A Blazor-aware table cell-rendering API equivalent in purpose to GCDS React's custom managed-slot table wrapper.
- An ASP.NET Core **Blazor Web App / Interactive Server** sample.
- JavaScript interop tests, wrapper/event coverage checks, a Visual Studio build script, and GitHub Actions CI.

## Solution layout

```text
GcdsBlazorComplete/
├── Gcds.Blazor.sln
├── build.ps1
├── pack.ps1
├── src/
│   └── Gcds.Blazor/
│       ├── Components/
│       │   ├── Generated/          # 41 GCDS component wrappers
│       │   ├── GcdsAssets.razor
│       │   ├── GcdsComponentBase.cs
│       │   └── GcdsEventArgs.cs
│       ├── Forms/                  # Blazor binding / EditContext bridge
│       ├── Interop/                # C# interop metadata
│       ├── Models/                 # checkbox/radio/table/suggestion models
│       ├── scripts/                # npm asset sync + coverage check
│       ├── wwwroot/
│       │   ├── gcds/               # generated from official npm dependency
│       │   └── js/gcds-blazor.js
│       ├── package.json
│       └── Gcds.Blazor.csproj
├── samples/
│   └── Gcds.Blazor.Demo/           # ASP.NET Core test host
├── tests/
│   └── js/
└── docs/
    └── COMPATIBILITY.md
```

## Open and run in Visual Studio

Prerequisites:

- Visual Studio 2022 with **ASP.NET and web development** installed.
- .NET 8 SDK.
- Node.js 20+ (Node 22 is used by CI).

Then:

1. Open `Gcds.Blazor.sln`.
2. Open **View > Terminal**.
3. Run:

```powershell
cd src\Gcds.Blazor
npm install
cd ..\..
dotnet build Gcds.Blazor.sln
```

`npm install` installs the pinned official dependency and automatically copies:

```text
node_modules/@gcds-core/components/dist/gcds
```

into:

```text
src/Gcds.Blazor/wwwroot/gcds
```

Set `Gcds.Blazor.Demo` as the startup project and run the `https` launch profile.

You can perform the normal setup/check/build sequence with:

```powershell
.\build.ps1
```

To create a NuGet package after the build passes:

```powershell
.\pack.ps1
```

## Add the assets to a Blazor Web App

For a .NET 8+ Blazor Web App, put `GcdsAssets` in the `<head>` of the root `App.razor`:

```razor
<head>
    ...
    <GcdsAssets />
    <HeadOutlet />
</head>
```

The component emits the static-web-asset references:

```text
_content/Gcds.Blazor/gcds/gcds.css
_content/Gcds.Blazor/gcds/gcds.esm.js
```

For older Blazor hosting models where the host HTML is not a Razor component, add equivalent `<link>` and `<script type="module">` elements directly to the host page.

## Basic component use

```razor
<GcdsButton ButtonRole="primary" OnGcdsClick="SaveClicked">
    Save
</GcdsButton>

<GcdsInput
    InputId="email"
    Name="email"
    Label="Email"
    Type="email"
    @bind-Value="model.Email" />
```

Every wrapper also accepts unmatched attributes, for example:

```razor
<GcdsButton class="my-class" aria-label="Save record">Save</GcdsButton>
```

## Complex GCDS properties

GCDS uses JavaScript properties for arrays and objects. The wrapper automatically assigns those after the custom element is upgraded instead of incorrectly stringifying them as HTML attributes.

```razor
<GcdsCheckboxes
    Name="features"
    Legend="Features"
    Options="@checkboxOptions"
    @bind-Value="selectedFeatures" />
```

```csharp
private readonly IReadOnlyList<GcdsCheckboxOption> checkboxOptions =
[
    new("feature-a", "Feature A", "a"),
    new("feature-b", "Feature B", "b")
];

private string[]? selectedFeatures;
```

Properties whose official TypeScript API accepts either JSON strings or arrays/objects are exposed as `object?`, so both forms remain possible.

## Custom events

Released 1.4.0 events are available directly:

```razor
<GcdsInput
    ...
    OnGcdsInput="InputChanged"
    OnGcdsSuggestionSelected="SuggestionSelected"
    OnGcdsValid="Valid"
    OnGcdsError="Invalid" />
```

The common event payload is:

```csharp
void InputChanged(GcdsEventArgs e)
{
    var value = e.GetDetail<string>();
}
```

You can also use `OnGcdsEvent` to receive every registered GCDS event. `AdditionalEventNames` lets a future GCDS custom event be listened to before a new wrapper version is released.

## Public component methods

Common form methods are exposed as C# methods:

```csharp
var valid = await input.CheckValidityAsync();
await input.ValidateAsync();
var validationMessage = await input.GetValidationMessageAsync();
```

Other examples include:

```csharp
await details.ToggleAsync();
await navGroup.ToggleNavAsync();
var rows = await table.GetVisibleRowsAsync();
```

For a future or uncommon GCDS method/property, every component inherits:

```csharp
await component.InvokeAsync("methodName", arg1, arg2);
var value = await component.GetPropertyAsync<string>("propertyName");
await component.SetPropertyAsync("propertyName", newValue);
```

## Blazor table cell templates

The official React package has special logic for framework-managed table cell slots. `Gcds.Blazor` provides the same kind of bridge through `GcdsTableColumn.RenderCell`.

```csharp
private static RenderFragment<GcdsTableCellContext> StrongCell => context => builder =>
{
    builder.OpenElement(0, "strong");
    builder.AddContent(1, context.Value);
    builder.CloseElement();
};

private readonly IReadOnlyList<GcdsTableColumn> columns =
[
    new("name", "Name"),
    new("value", "Value") { RenderCell = StrongCell }
];
```

```razor
<GcdsTable Columns="@columns" Data="@rows" Sort="true" Pagination="true" />
```

The wrapper automatically sets GCDS's internal `managed` column flag and generates the expected `cell-{rowKey}-{field}` named slots.

## Dependency/update strategy

The npm dependency is intentionally pinned:

```json
"@gcds-core/components": "1.4.0"
```

Do not edit `wwwroot/gcds` manually. To upgrade GCDS:

1. Change the version in `src/Gcds.Blazor/package.json`.
2. Run `npm install`.
3. Update the compatibility baseline/expected component list if the API changed.
4. Run `build.ps1`.
5. Run the demo and browser-check the affected components.
6. Commit the resulting `package-lock.json` once it is generated on a networked development machine.

The wrapper keeps `AdditionalAttributes`, `Properties`, generic method/property interop, and `AdditionalEventNames` as escape hatches so many additive GCDS changes can be used before the wrapper is regenerated.

## Important limitations

### File uploads

`GcdsFileUploader` wraps the official GCDS file uploader and its serializable value/events. It **does not yet expose selected browser files as Blazor `IBrowserFile` streams**. A dedicated file-stream bridge should be implemented and browser-tested before treating it as a replacement for Blazor `InputFile`.

### JavaScript validator functions

Built-in/string validator configuration can cross the normal interop boundary. Arbitrary JavaScript validator functions cannot be represented by a normal C# delegate through JSON serialization; advanced custom validators need a JS interop object/function reference or application-side validation.

## Validation performed when this artifact was generated

The generation environment had Node.js but did **not** have the .NET SDK and could not reach the npm registry. Therefore the checks completed here were:

- 41/41 wrapper catalog coverage.
- 12/12 released GCDS custom event names registered.
- JavaScript bridge tests: complex properties, event forwarding, component method invocation, property get/set, and listener cleanup.
- Static C# brace/surface checks.
- A Razor smoke page that instantiates every wrapper.
- Review against the released GCDS 1.4.0 `components.d.ts` surface and the framework wrapper configuration.

A real `dotnet restore` / `dotnet build` and browser run must still be completed on a machine with .NET 8 and npm access. The included `build.ps1` and CI workflow are set up to do exactly that.
