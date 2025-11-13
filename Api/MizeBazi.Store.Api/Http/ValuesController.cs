using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using MizeBazi.Store.Api.Helper;
using MizeBazi.Store.Application;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Services.Grpc;

namespace MizeBazi.Store.Api.Http;

[ApiController]
public class ValuesController : ControllerBase
{

    [HttpPost("api/Create")]
    public async Task<IActionResult> Create(
        [FromBody] CreateProductCommand command,
        [FromServices] IAppMediator mediator
    )
    {
        var id = await mediator.Send(command);
        return Ok(new { Id = id });
    }
    [HttpPost("api/grpc")]
    public async Task<IActionResult> grpc(string token)
    {
        var t = await new UserGrpcService().CheckToken(token);
        return Ok(t);
    }
}
