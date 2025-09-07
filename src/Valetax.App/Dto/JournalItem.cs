namespace Valetax.App.Dto;

public class JournalItem
{
    public required int Id { get; set; }
    
    public required Guid EventId { get; set; }
    
    public required string MethodName { get; set; }
    
    public string? Query { get; set; }
    
    public string? Body { get; set; }
    
    public string? StackTrace { get; set; }
    
    public required DateTimeOffset CreatedAt { get; set; }
}
