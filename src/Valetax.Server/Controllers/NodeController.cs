using Microsoft.AspNetCore.Mvc;
using Valetax.App.Services;
using Valetax.Server.Api;

namespace Valetax.Server.Controllers;

public class NodeController : ApiControllerBase
{
    private readonly NodeService _nodeService;

    public NodeController(NodeService nodeService)
    {
        _nodeService = nodeService;
    }

    [HttpPost("create")]
    public async Task<ActionResult<Response<int>>> Create(NodeCreateModel model)
    {
        var id = await _nodeService.Create(model.TreeId, model.ParentNodeId,  model.Name);

        var response = new Response<int>
        {
            Data = id,
            Success = true,
            Message = "OK"
        };
        return Ok(response);
    }
    
    [HttpPut("update")]
    public async Task<ActionResult<Response>> Create(NodeUpdateModel model)
    {
        await _nodeService.Update(model.NodeId, model.Name);

        var response = new Response
        {
            Success = true,
            Message = "OK"
        };
        
        return Ok(response);
    }

    [HttpDelete]
    public async Task<ActionResult<Response>> Delete([FromQuery] int id)
    {
        await _nodeService.Delete(id);
        var response = new Response
        {
            Success = true,
            Message = "OK"
        };
        
        return Ok(response);
    }
}
