using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MizeBazi.Store.Api.Middleware;
using MizeBazi.Store.Application;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Api.Http;

public class ValuesController(IAppLogger<ValuesController> logger) : _ControllerBase
{
    private readonly IAppLogger<ValuesController> _logger = logger;

    [HttpPost("Create")]
    public async Task<IActionResult> Create(
        [FromBody] CreateProductCommand command,
        [FromServices] IAppMediator mediator
    )
    {
        _logger.LogInformation("Create {Time}", DateTime.UtcNow);
        var id = await mediator.Send(command);
        return Ok(new { Id = id });
    }

    [HttpPost("grpca")]
    public async Task<IActionResult> Grpca(
         UserRoles role,
        [FromServices] ICacheService cacheService
    )
    {
        //var t = await new UserGrpcService().CheckToken(token);
        var userId = Guid.NewGuid();
        var model = new
        {
            Id = Guid.NewGuid(),
            UserId= userId
            //UserId = 15,
            //Role = role,
        };

        var tokens = cacheService.SearchByPartition<CacheInMemoryRecord>("token",
            t => t.UserId == 45
        ).ToList();
        if (tokens.Count > 0)
        {
            foreach (var t in tokens) cacheService.Remove(t.Key);
        }

        var cacheModel = new CacheInMemoryRecord(Guid.NewGuid(), model.Id, 45, role);
        cacheService.Set("token", cacheModel.Id, cacheModel, 5, 30);

        //await Task.Delay(500);
        
        //cacheService.TryGetValue<CacheInMemoryRecord>("token", cacheModel.Id, out var lastCallTime);

        //_logger.LogWarning($"Grpca LogWarning, cacheModel.Id {cacheModel.Id}");
        return Ok(model);
    }


    [Auth(UserRoles.Guest), HttpPost("Auth0")]
    public IActionResult Auth0() => Ok(new { Id = "Auth0" });

    [Auth(UserRoles.Customer), HttpPost("Auth1")]
    public IActionResult Auth1() => Ok(new { Id = "Auth1" });

    [Auth(UserRoles.Admin), HttpPost("Auth101")]
    public IActionResult Auth101() => Ok(new { Id = "Auth101" });
}
