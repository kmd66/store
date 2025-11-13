using Coravel.Cache.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using MizeBazi.Store.Application;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;

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
    [HttpPost("api/grpca")]
    public async Task<IActionResult> Grpca(
        //string token,
        [FromServices] ICacheService cacheService
    )
    {
        //var t = await new UserGrpcService().CheckToken(token);

        var model = new Jwt
        {
            Id = Guid.NewGuid(),
            UserId= 15,
            Role = UserRoles.Admin,
        };

        var tokens = cacheService.SearchByPartition<CacheInMemoryRecord>("token",
            t => t.UserId == model.UserId
        ).ToList();
        if (tokens.Count > 0)
        {
            foreach(var t in tokens) cacheService.Remove(t.Key);
        }

        var cacheModel = new CacheInMemoryRecord(Guid.NewGuid(), model.Id, model.UserId, model.Role);
        cacheService.Set("token", cacheModel.Id, cacheModel, 5, 30);

        await Task.Delay(5000);
        
        cacheService.TryGetValue<CacheInMemoryRecord>("token", cacheModel.Id, out var lastCallTime);

        return Ok(lastCallTime);
    }
}
