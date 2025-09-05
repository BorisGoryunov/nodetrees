namespace Valetax.Server.Api;

public class Response
{
    public bool Success { get; init; }

    public required string Message { get; init; } 
}

public class Response<T>
{
    public required T Data { get; init; }
    
    public bool Success { get; init; }

    public required string Message { get; init; } 
}
