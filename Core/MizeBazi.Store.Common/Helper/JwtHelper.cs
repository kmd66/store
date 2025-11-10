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
        string jsonString = model.ToJson();
        return jsonString.AesEncrypt(AppSetings.JwtKey, AppSetings.JwtIv);
    }

    public Jwt Decode(string token)
    {
        try
        {
            if (token.IsNullOrEmpty())
                return null;
            var aesDecrypt = token.AesDecrypt(AppSetings.JwtKey, AppSetings.JwtIv);
            var model = aesDecrypt.JsonToObject<Jwt>();
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