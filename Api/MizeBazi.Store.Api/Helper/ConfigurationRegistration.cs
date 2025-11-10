
using MizeBazi.Store.Common.Helper;

namespace MizeBazi.Store.Api.Helper;
public static class ConfigurationRegistration
{
    public static IConfiguration SetAppSetings(this IConfiguration config)
    {
        AppSetings.SetConnection();

        AppSetings.JwtKey = config.GetSection("Jwt:Key").Value!;
        AppSetings.JwtIv = config.GetSection("Jwt:Iv").Value!;
        AppSetings.AccessTokenTime = int.Parse(config.GetSection("Jwt:AccessTokenTime").Value!);


        return config;
    }

}
