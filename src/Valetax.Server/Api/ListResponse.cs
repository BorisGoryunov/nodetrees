namespace Valetax.Server.Api;

public class ListResponse<T>
{
    public required IReadOnlyList<T> Data { get; init; }
    
    public bool Success { get; init; }

    public required string Message { get; init; } 
}
