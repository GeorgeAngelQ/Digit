using Microsoft.Extensions.Configuration;

namespace Libreria;

[CLSCompliant(true)]
public static class ConfigurationJson
{
    public static string GetAppSettings(string key)
    {
        try
        {

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            var itemsConfig = config.GetSection("AppSettings")
                .GetChildren().ToDictionary(x => x.Key, x => x.Value);

            return itemsConfig[key];
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static string GetConnectionStrings(string key)
    {
        try
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            var itemsConfig = config.GetSection("ConnectionStrings")
                .GetChildren().ToDictionary(x => x.Key, x => x.Value);

            return itemsConfig[key];
        }
        catch (Exception)
        {
            return null;
        }
    }
}