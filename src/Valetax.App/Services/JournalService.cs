using Microsoft.EntityFrameworkCore;
using Valetax.App.Dto;
using Valetax.Entities;
using Valetax.Persistence;

namespace Valetax.App.Services;

public class JournalService
{
    private readonly AppDbContext _dbContext;

    public JournalService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<JournalDto?> Read(int id)
    {
        var data = await _dbContext.Set<Journal>()
            .Where(x => x.Id == id)
            .Select(x => new JournalDto
            {
                MethodName = x.MethodName,
                StackTrace = x.StackTrace,
                EventId = x.EventId,
                Body = x.Body,
                Query = x.Query,
                CreatedAt = x.CreatedAt
            })
            .SingleOrDefaultAsync();
        
        return data;
    }
}
