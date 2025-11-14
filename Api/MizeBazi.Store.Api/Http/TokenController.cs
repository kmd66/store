using Microsoft.AspNetCore.Mvc;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Services.Grpc;

namespace MizeBazi.Store.Api.Http;

public class TokenController(
    IAppLogger<TokenController> logger,
     ICacheService cacheService
) : _ControllerBase
{
    private readonly IAppLogger<TokenController> _logger = logger;
    private readonly ICacheService _cacheService = cacheService;

    [HttpPost("GetToken")]
    public async Task<Result<dynamic>> GetToken([FromBody] TokenRequest model)
    {
        var result = await new UserGrpcService().CheckToken(model.Token);
        if(!result.success)
            return Result<dynamic>.Failure(message:result.message, code:401);
        var resultModel = SetTokenInCache(result.data);

        //var resultModel = SetTokenInCache(new Jwt { Id = Guid.NewGuid(), Role = UserRoles.Admin, UserId = 20 });

        return Result<dynamic>.Successful(data: resultModel);
    }

    [HttpPost("refreshToken")]
    public Task<Result<dynamic>> RefreshToken([FromBody] TokenRequest model)
        => GetToken(model);

    private dynamic SetTokenInCache(Jwt model)
    {
        var resultModel = new
        {
            UserId = Guid.NewGuid(),
            TokenId = model.Id,
            model.Role,
        };

        var tokens = _cacheService.SearchByPartition<CacheInMemoryRecord>("token",
            t => t.UserId == model.UserId
        ).ToList();
        if (tokens.Count > 0)
        {
            foreach (var t in tokens) _cacheService.Remove(t.Key);
        }

        var cacheModel = new CacheInMemoryRecord(resultModel.TokenId, model.UserId, model.Role);
        _cacheService.Set("token", resultModel.UserId, cacheModel, 5, 30);
        return resultModel;
    }
}
