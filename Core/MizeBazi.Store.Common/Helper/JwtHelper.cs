using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Common.Helper;

public class JwtHelper
{

    public string Code(Guid id, long userId, string deviceId)
    {
        var model = new Jwt
        {
            Id = id,
            Date = DateTime.Now,
            Expiry = DateTime.Now.AddMonths(AppSetings.AccessTokenTime),
            UserId = userId,
            DeviceId = deviceId
        };
        return code(model);

    }
    public string Code(long userId, string deviceId)
    {
        var model = new Jwt
        {
            Id = Guid.NewGuid(),
            Date = DateTime.Now,
            Expiry = DateTime.Now.AddMonths(AppSetings.AccessTokenTime),
            UserId = userId,
            DeviceId = deviceId
        };
        return code(model);

    }
    private string code(Jwt model)
    {
        string jsonString = System.Text.Json.JsonSerializer.Serialize(model);
        return jsonString.AesEncrypt(AppSetings.JwtKey, AppSetings.JwtIv);
    }

    public Jwt Decode(string token)
    {
        try
        {
            if (token.IsNullOrEmpty())
                return null;

            var model = System.Text.Json.JsonSerializer.Deserialize<Jwt>(token.AesDecrypt(AppSetings.JwtKey, AppSetings.JwtIv));
            if (model.Expiry < DateTime.Now)
                return null;
            return model;
        }
        catch
        {
            return null;
        }
    }

}