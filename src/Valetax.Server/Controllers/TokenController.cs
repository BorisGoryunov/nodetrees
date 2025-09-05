using Microsoft.AspNetCore.Mvc;
using Valetax.App.Services;
using Valetax.Server.Api;

namespace Valetax.Server.Controllers;

public class TokenController : ApiControllerBase
{
    private readonly TokenService _tokenService;

    public TokenController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpGet("{code}")]
    public ActionResult<Response<string>> GetToken(string code)
    {
        var token = _tokenService.GetToken(code);
        
        var response = new Response<string>
        {
            Data = token,
            Message = "OK",
            Success = true
        };

        return Ok(response);
    }
}
