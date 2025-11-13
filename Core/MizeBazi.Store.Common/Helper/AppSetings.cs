using System.Text.Json;

namespace MizeBazi.Store.Common.Helper;
public static class AppSetings
{
    public static void SetConnection()
    {
        if (WriteConnection.IsNullOrEmpty())
        {
            try
            {
                
                //var configPaths = Path.Combine(Directory.GetCurrentDirectory(), "connection.json");
                var configPath = Path.Combine(AppContext.BaseDirectory, "connection.json");

                if (File.Exists(configPath))
                {
                    var jsonContent = File.ReadAllText(configPath);
                    var jsonData = jsonContent.JsonToObject<JsonElement>();

                    WriteConnection = jsonData.GetProperty("WriteConnection").GetString();
                    ReadConnection = jsonData.GetProperty("ReadConnection").GetString();
                }
                else
                {
                    throw new FileNotFoundException($"connection.json not found at {configPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[StoreContext] Failed to read connection.json: {ex.Message}");
                throw;
            }
        }
    }
    public static bool IsDevelopment { get; set; }

    public static string WriteConnection { get; set; }
    public static string ReadConnection { get; set; }

    public static string CreateRoomKey { get; set; }
    public static string JwtKey { get; set; }
    public static string JwtIv { get; set; }
    public static int AccessTokenTime { get; set; }

    public static string UrlApi { get; set; }
    public static int ApiPort { get; set; }
    public static int GrpcPort { get; set; }
    public static string GrpcGgateway { get; set; }
    public static string UrlGateway { get; set; }

}
