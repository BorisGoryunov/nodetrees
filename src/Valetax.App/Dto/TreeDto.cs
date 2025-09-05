namespace Valetax.App.Dto;

public class TreeDto
{
    public required int Id { get; init; }
    
    public required string Name { get; init; }
    
    public required IReadOnlyList<string> Children { get; init; }
}
