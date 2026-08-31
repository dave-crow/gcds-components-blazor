using System.Text.Json.Serialization;

namespace Gcds.Blazor.Models;

public sealed record GcdsCheckboxOption(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("label")] string Label,
    [property: JsonPropertyName("value")] string? Value = null,
    [property: JsonPropertyName("hint")] string? Hint = null,
    [property: JsonPropertyName("checked")] bool? Checked = null);

public sealed record GcdsRadioOption(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("label")] string Label,
    [property: JsonPropertyName("value")] string Value,
    [property: JsonPropertyName("hint")] string? Hint = null,
    [property: JsonPropertyName("checked")] bool? Checked = null);

public sealed record GcdsSuggestionOption(
    [property: JsonPropertyName("label")] string Label,
    [property: JsonPropertyName("value")] string? Value = null);

public sealed record GcdsTableColumn(
    [property: JsonPropertyName("field")] string Field,
    [property: JsonPropertyName("header")] string Header,
    [property: JsonPropertyName("sortDirection")] string? SortDirection = null,
    [property: JsonPropertyName("alignment")] string? Alignment = null,
    [property: JsonPropertyName("sort")] bool? Sort = null,
    [property: JsonPropertyName("rowHeader")] bool? RowHeader = null,
    [property: JsonPropertyName("slotted")] bool? Slotted = null,
    [property: JsonPropertyName("managed")] bool? Managed = null)
{
    /// <summary>Optional Blazor cell renderer. When present the wrapper creates the managed GCDS cell slot.</summary>
    [JsonIgnore]
    public Microsoft.AspNetCore.Components.RenderFragment<GcdsTableCellContext>? RenderCell { get; init; }
}

public sealed record GcdsTableCellContext(
    object Row,
    int RowIndex,
    GcdsTableColumn Column,
    object? Value);

public sealed record GcdsTableVisibleRow(
    [property: JsonPropertyName("rowId")] string RowId,
    [property: JsonPropertyName("rowIndex")] int RowIndex,
    [property: JsonPropertyName("original")] System.Text.Json.JsonElement Original);

public sealed record GcdsTableSorting(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("desc")] bool Desc);

public sealed record GcdsTableStateChange(
    [property: JsonPropertyName("visibleRows")] IReadOnlyList<GcdsTableVisibleRow> VisibleRows,
    [property: JsonPropertyName("page")] int Page,
    [property: JsonPropertyName("pageSize")] int PageSize,
    [property: JsonPropertyName("filterValue")] string FilterValue,
    [property: JsonPropertyName("sorting")] IReadOnlyList<GcdsTableSorting>? Sorting);
