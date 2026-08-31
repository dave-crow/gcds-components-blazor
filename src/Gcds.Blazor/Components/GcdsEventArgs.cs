using System.Text.Json;

namespace Gcds.Blazor.Components;

public sealed class GcdsEventArgs : EventArgs
{
    public GcdsEventArgs(string name, JsonElement? detail) { Name = name; Detail = detail; }
    public string Name { get; }
    public JsonElement? Detail { get; }
    public T? GetDetail<T>() => Detail is null ? default : Detail.Value.Deserialize<T>();
}
