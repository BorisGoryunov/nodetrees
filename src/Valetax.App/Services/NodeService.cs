using Microsoft.EntityFrameworkCore;
using Valetax.App.Exceptions;
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

    public async ValueTask<int> Create(int treeId, int? parentNodeId, string name)
    {
        await Validate(treeId, parentNodeId);   
        
        var node = new Node
        {
            TreeId = treeId,
            ParentId = parentNodeId,
            Name = name,
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
            .Where(x => x.Id == nodeId)
            .ExecuteDeleteAsync();
    }

    private async Task Validate(int treeId, int? parentNodeId)
    {
        if (!parentNodeId.HasValue)
        {
            return;
        }

        var parentAny = await _dbContext.Set<Node>()
            .Where(x => x.Id == parentNodeId)
            .Where(x => x.TreeId == treeId)
            .AnyAsync();

        if (parentAny)
        {
            return;
        }

        throw new SecureException($"treeId = {treeId}, parentNodeId = {parentNodeId} not found.");
    }
}
