using Microsoft.EntityFrameworkCore;
using Valetax.App.Dto;
using Valetax.App.Exceptions;
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

    public async Task<Guid> Create(string methodName, string query,  string body, string? stackTrace)
    {
        var item = new Journal
        {
            CreatedAt = DateTimeOffset.UtcNow,
            EventId = Guid.NewGuid(),
            MethodName = methodName,
            Body = body,
            Query = query,
            StackTrace = stackTrace
        };

        await _dbContext.AddAsync(item);
        await _dbContext.SaveChangesAsync();
        
        return item.EventId;
    }
    
    public async Task<JournalDto> Read(int id)
    {
        var item = await _dbContext.Set<Journal>()
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

        if (item is null)
        {
            throw new SecureException("Данные не найдены.");
        }
        
        return item;
    }
    
    public async Task<IReadOnlyList<JournalDto>> GetRange(int offset,
        int limit,
        DateTimeOffset? fromDate,
        DateTimeOffset? toDate)
    {
        var query = _dbContext.Set<Journal>()
            .AsQueryable();

        if (fromDate.HasValue)
        {
            query = query.Where(x => x.CreatedAt >= fromDate.Value);
        }

        if (toDate.HasValue)
        {
            query = query.Where(x => x.CreatedAt <= toDate.Value);
        }

        var data = await query
            .Skip(offset)
            .Take(limit)
            .Select(x => new JournalDto
            {
                MethodName = x.MethodName,
                StackTrace = x.StackTrace,
                EventId = x.EventId,
                Body = x.Body,
                Query = x.Query,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();
        
        return data;
    }

}
