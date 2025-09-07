using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Valetax.App.Dto;
using Valetax.App.Exceptions;
using Valetax.Entities;
using Valetax.Persistence;

namespace Valetax.App.Services;

public class JournalService
{
    private readonly AppDbContext _dbContext;
    private readonly IServiceProvider _serviceProvider;

    public JournalService(AppDbContext dbContext, IServiceProvider serviceProvider)
    {
        _dbContext = dbContext;
        _serviceProvider = serviceProvider;
    }

    public async Task<Guid> Create(string methodName, string query,  string body, string? stackTrace)
    {
        var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        var item = new Journal
        {
            CreatedAt = DateTimeOffset.UtcNow,
            EventId = Guid.NewGuid(),
            MethodName = methodName,
            Body = body,
            Query = query,
            StackTrace = stackTrace
        };

        await dbContext.AddAsync(item);
        await dbContext.SaveChangesAsync();
        
        return item.EventId;
    }
    
    public async Task<JournalItem> Read(int id)
    {
        var item = await _dbContext.Set<Journal>()
            .Where(x => x.Id == id)
            .Select(x => new JournalItem
            {
                Id = x.Id,
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

    public async Task<JournalItem> Read(Guid eventId)
    {
        var item = await _dbContext.Set<Journal>()
            .Where(x => x.EventId == eventId)
            .Select(x => new JournalItem
            {
                Id = x.Id,
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
    
    public async Task<IReadOnlyList<JournalItem>> GetRange(int offset,
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
            .Select(x => new JournalItem
            {
                Id = x.Id,
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
