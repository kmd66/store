
using MizeBazi.Store.Common.Helper;

namespace MizeBazi.Store.Api.Filters;
public static class ConfigurationRegistration
{
    public static IConfiguration SetAppSetings(this IConfiguration config)
    {
        AppSetings.WriteConnection = config.GetSection("ConnectionStrings:WriteConnection").Value;
        AppSetings.ReadConnection = config.GetSection("ConnectionStrings:ReadConnection").Value;

        AppSetings.JwtKey = config.GetSection("Jwt:Key").Value!;
        AppSetings.JwtIv = config.GetSection("Jwt:Iv").Value!;
        AppSetings.AccessTokenTime = int.Parse(config.GetSection("Jwt:AccessTokenTime").Value!);


        return config;
    }

}
