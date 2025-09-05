namespace Valetax.Entities;

#nullable disable

public class Node
{
    public int Id { get; set; }
    
    public required string Name { get; set; }
    
    public int? ParentId { get; set; }
    
    public Node Parent { get; set; }
    
    public required int TreeId { get; set; }    
    
    public Tree Tree { get; set; }
}
