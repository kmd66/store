
using Microsoft.AspNetCore.Server.Kestrel.Core;
using MizeBazi.Store.Common.Helper;

namespace MizeBazi.Store.Api.Helper;
public static class GrpcRegistration
{
    public static WebApplicationBuilder SetGrpc(this WebApplicationBuilder b)
    {
        b.WebHost.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(AppSetings.ApiPort, o => o.Protocols = HttpProtocols.Http1);
            options.ListenAnyIP(AppSetings.GrpcPort, o => o.Protocols = HttpProtocols.Http2);

            //options.ListenAnyIP(AppSetings.ApiPort + 1, o => { o.Protocols = HttpProtocols.Http1; o.UseHttps(); });
            //options.ListenAnyIP(AppSetings.GrpcPort + 1, listenOptions => { listenOptions.Protocols = HttpProtocols.Http2; listenOptions.UseHttps(); });
        });

        AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
        b.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        b.Services.AddEndpointsApiExplorer();
        b.Services.AddSwaggerGen();
        b.Services.AddHttpContextAccessor();
       
        b.Services.AddGrpc();

        return b;
    }

}
