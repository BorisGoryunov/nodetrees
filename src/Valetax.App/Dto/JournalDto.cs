namespace Valetax.App.Dto;

public class JournalDto
{
    public required string MethodName { get; set; }
    
    public string? Query { get; set; }
    
    public string? Body { get; set; }
    
    public required string StackTrace { get; set; }
    
    public required Guid EventId { get; set; }
    
    public required DateTimeOffset CreatedAt { get; set; }
}
