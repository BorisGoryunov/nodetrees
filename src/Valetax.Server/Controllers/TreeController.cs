using Microsoft.AspNetCore.Mvc;
using Valetax.App.Dto;
using Valetax.App.Services;
using Valetax.Server.Api;

namespace Valetax.Server.Controllers;

public class TreeController : ApiControllerBase
{
    private readonly TreeService _service;

    public TreeController(TreeService  service)
    {
        _service = service;
    }

    [HttpPost("create")]
    public async Task<ActionResult<Response<int>>> Create(TreeCreateModel model)
    {
        var id = await _service.Create(model.Name);

        var response = new Response<int>
        {
            Data = id,
            Success = true,
            Message = "OK"
        };
        
        return Ok(response);    
    }
    
    [HttpGet("{name}")]
    public async Task<ActionResult<ListResponse<TreeDto>>> Create(string name)
    {
        var data = await _service.Read(name);

        var response = new ListResponse<TreeDto>
        {
            Data = data,
            Success = true,
            Message = "OK"
        };
        
        return Ok(response);    
    }
}
