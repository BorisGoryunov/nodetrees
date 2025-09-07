using Microsoft.AspNetCore.Mvc;
using Valetax.App.Dto;
using Valetax.App.Services;
using Valetax.Server.Api;

namespace Valetax.Server.Controllers;

public class JournalController : ApiControllerBase
{
    private readonly JournalService _journalService;

    public JournalController(JournalService journalService)
    {
        _journalService = journalService;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<JournalItem>>> Read(int id)
    {
        var item = await _journalService.Read(id);

        var response = new Response<JournalItem>
        {
            Data = item,
            Success = true,
            Message = "OK"
        };

        return Ok(response);
    }

    [HttpGet("event/{eventId:guid}")]
    public async Task<ActionResult<Response<JournalItem>>> Read(Guid eventId)
    {
        var item = await _journalService.Read(eventId);

        var response = new Response<JournalItem>
        {
            Data = item,
            Success = true,
            Message = "OK"
        };

        return Ok(response);
    }
    
    [HttpGet("range")]
    public async Task<ActionResult<ListResponse<JournalItem>>> GetRange([FromQuery] int offset,
        [FromQuery] int limit,
        [FromQuery] DateTimeOffset? fromDate,
        [FromQuery] DateTimeOffset? toDate )
    {
        var data = await _journalService.GetRange(offset, limit, fromDate, toDate);
        
        var response = new ListResponse<JournalItem>
        {
            Data = data,
            Success = true,
            Message = "OK"
        };

        return Ok(response);
    }
}
