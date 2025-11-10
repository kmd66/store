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
                
                var configPath = Path.Combine(Directory.GetCurrentDirectory(), "connection.json");

                if (File.Exists(configPath))
                {
                    var jsonContent = File.ReadAllText(configPath);
                    var jsonData = JsonSerializer.Deserialize<JsonElement>(jsonContent);

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

    public static string WriteConnection { get; set; }
    public static string ReadConnection { get; set; }
    public static string JwtKey { get; set; }
    public static string JwtIv { get; set; }
    public static int AccessTokenTime { get; set; }

}