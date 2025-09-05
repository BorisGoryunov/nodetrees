using Microsoft.EntityFrameworkCore;
using Valetax.Entities;
using Valetax.Persistence;

namespace Valetax.App.Services;

public class NodeService
{
    private readonly AppDbContext _dbContext;

    public NodeService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Create(int treeId, int? parentNodeId, string name)
    {
        var node = new Node
        {
            TreeId = treeId,
            Name = name,
            ParentId = parentNodeId,
        };

        await _dbContext.AddAsync(node);
        await _dbContext.SaveChangesAsync();
        
        return node.Id;
    }

    public async Task Update(int nodeId, string newNodeName)
    {
        await _dbContext.Set<Node>()
            .Where(x => x.Id == nodeId)
            .ExecuteUpdateAsync(x => x
                .SetProperty(node => node.Name, newNodeName));
    }

    public async Task Delete(int nodeId)
    {
        await _dbContext.Set<Node>()
            .Where(x=>x.Id == nodeId)
            .ExecuteDeleteAsync();
    }
            
}
