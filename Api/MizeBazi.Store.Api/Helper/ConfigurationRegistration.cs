
using MizeBazi.Store.Common.Helper;

namespace MizeBazi.Store.Api.Helper;
public static class ConfigurationRegistration
{
    public static IConfiguration SetAppSetings(this IConfiguration config)
    {
        AppSetings.SetConnection();

        AppSetings.CreateRoomKey = config.GetSection("Jwt:createRoomKey").Value;
        AppSetings.JwtKey = config.GetSection("Jwt:Key").Value;
        AppSetings.JwtIv = config.GetSection("Jwt:Iv").Value;
        AppSetings.AccessTokenTime = int.Parse(config.GetSection("Jwt:AccessTokenTime").Value);

        AppSetings.ApiPort = int.Parse(config.GetSection("Urls:apiPort").Value);
        AppSetings.GrpcPort = int.Parse(config.GetSection("Urls:grpcPort").Value);
        AppSetings.UrlApi = $"{config.GetSection("Urls:api").Value}:{AppSetings.ApiPort}";
        AppSetings.UrlGateway = config.GetSection("Urls:gateway").Value;
        AppSetings.GrpcGgateway = config.GetSection("Urls:grpcGgateway").Value;

        return config;
    }

}

