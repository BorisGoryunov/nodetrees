using Microsoft.EntityFrameworkCore;
using Valetax.App.Dto;
using Valetax.Entities;
using Valetax.Persistence;

namespace Valetax.App.Services;

public class TreeService
{
    private readonly AppDbContext _dbContext;

    public TreeService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async ValueTask<int> Create(string name)
    {
        var tree = new Tree
        {
            Name = name
        };

        await _dbContext.AddAsync(tree);
        await _dbContext.SaveChangesAsync();
        
        return tree.Id;
    }

    public async Task<IReadOnlyList<TreeDto>> Read(string name)
    {
        var data = await _dbContext.Set<Tree>()
            .Where(x => x.Name.Contains(name))
            .Select(x => new TreeDto
            {
                Id = x.Id,
                Name = x.Name,
                Children = _dbContext.Set<Node>()
                    .Where(y => y.TreeId == x.Id)
                    .Select(y => y.Name)
                    .ToList(),
            })
            .ToListAsync();
        
        return data;
    }
}
