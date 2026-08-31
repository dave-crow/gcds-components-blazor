using System.Collections;
using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Gcds.Blazor.Models;

namespace Gcds.Blazor.Components;

public sealed class GcdsTable : GcdsComponentBase
{
    protected override string TagName => "gcds-table";

    /// <summary>
    /// Accepts either a JSON string or a collection of <see cref="GcdsTableColumn"/> values.
    /// A column with RenderCell set is automatically marked as a framework-managed slot.
    /// </summary>
    [Parameter] public object? Columns { get; set; }

    /// <summary>Accepts either a JSON string or an enumerable of row objects.</summary>
    [Parameter] public object? Data { get; set; }
    [Parameter] public bool? Filter { get; set; }
    [Parameter] public string? FilterValue { get; set; }
    [Parameter] public bool? Pagination { get; set; }
    [Parameter] public int? PaginationCurrentPage { get; set; }
    [Parameter] public int? PaginationSize { get; set; }
    [Parameter] public object? PaginationSizeOptions { get; set; }
    [Parameter] public bool? Sort { get; set; }
    [Parameter] public RenderFragment? Caption { get; set; }

    protected override IEnumerable<KeyValuePair<string, object?>> GetComponentParameters()
    {
        if (Columns is not null) yield return new("columns", PrepareColumns());
        if (Data is not null) yield return new("data", Data);
        if (Filter is not null) yield return new("filter", Filter);
        if (FilterValue is not null) yield return new("filter-value", FilterValue);
        if (Pagination is not null) yield return new("pagination", Pagination);
        if (PaginationCurrentPage is not null) yield return new("pagination-current-page", PaginationCurrentPage);
        if (PaginationSize is not null) yield return new("pagination-size", PaginationSize);
        if (PaginationSizeOptions is not null) yield return new("pagination-size-options", PaginationSizeOptions);
        if (Sort is not null) yield return new("sort", Sort);
    }

    protected override void BuildChildContent(RenderTreeBuilder builder)
    {
        base.BuildChildContent(builder);

        if (Caption is not null)
        {
            builder.OpenElement(4, "span");
            builder.AddAttribute(5, "slot", "caption");
            builder.AddContent(6, Caption);
            builder.CloseElement();
        }

        var columns = GetTypedColumns();
        var rows = GetRows();
        if (columns is null || rows is null) return;

        for (var rowIndex = 0; rowIndex < rows.Count; rowIndex++)
        {
            var row = rows[rowIndex];
            foreach (var column in columns)
            {
                if (column.RenderCell is null) continue;

                var slotName = GetSlotName(row, column, rowIndex);
                var context = new GcdsTableCellContext(row, rowIndex, column, GetMemberValue(row, column.Field));

                builder.OpenElement(7, "span");
                builder.AddAttribute(8, "slot", slotName);
                builder.AddContent(9, column.RenderCell(context));
                builder.CloseElement();
            }
        }
    }

    private object PrepareColumns()
    {
        var typed = GetTypedColumns();
        if (typed is null) return Columns!;
        return typed.Select(column => column.RenderCell is null ? column : column with { Managed = true }).ToArray();
    }

    private IReadOnlyList<GcdsTableColumn>? GetTypedColumns()
    {
        if (Columns is IReadOnlyList<GcdsTableColumn> readOnly) return readOnly;
        if (Columns is IEnumerable<GcdsTableColumn> enumerable) return enumerable.ToArray();
        return null;
    }

    private IReadOnlyList<object>? GetRows()
    {
        if (Data is string or null) return null;
        if (Data is IReadOnlyList<object> readOnly) return readOnly;
        if (Data is IEnumerable enumerable) return enumerable.Cast<object>().ToArray();
        return null;
    }

    private static string GetSlotName(object row, GcdsTableColumn column, int rowIndex)
    {
        var id = GetMemberValue(row, "id");
        var key = id is null || string.IsNullOrWhiteSpace(Convert.ToString(id))
            ? rowIndex.ToString()
            : Convert.ToString(id)!;
        return $"cell-{key}-{column.Field}";
    }

    private static object? GetMemberValue(object row, string field)
    {
        if (row is JsonElement json && json.ValueKind == JsonValueKind.Object && json.TryGetProperty(field, out var value))
            return value;

        if (row is IReadOnlyDictionary<string, object?> readOnlyDictionary && readOnlyDictionary.TryGetValue(field, out var roValue))
            return roValue;

        if (row is IDictionary<string, object?> dictionary && dictionary.TryGetValue(field, out var valueObject))
            return valueObject;

        if (row is IDictionary nonGeneric && nonGeneric.Contains(field))
            return nonGeneric[field];

        var property = row.GetType().GetProperty(field, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
        return property?.GetValue(row);
    }

    public ValueTask<GcdsTableVisibleRow[]> GetVisibleRowsAsync() =>
        InvokeAsync<GcdsTableVisibleRow[]>("getVisibleRows");
}
