using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Common.Helper;
using Grpc.Net.Client;
using System;
using Grpc.Core;

namespace MizeBazi.Store.Services.Grpc;

public class UserGrpcService : UserGrpc.UserGrpcBase
{
    public async Task<Result<Jwt>> CheckToken(string token)
    {
        var channel = GrpcChannel.ForAddress(AppSetings.GrpcGgateway);
        var client = new UserGrpc.UserGrpcClient(channel);
        var request = new DataToken { Token = token };
        var response = await client.TokenDecodeAsync(request);
        if (!response.Result.Success)
            return Result<Jwt>.Failure(code: response.Result.Code, message: response.Result.Message);
        var model = response.JsonMapObject<Jwt>();
        model.Role = (UserRoles)response.Roles;
        return Result<Jwt>.Successful(data: model);
    }
}