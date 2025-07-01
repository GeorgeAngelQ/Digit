using Microsoft.Extensions.Configuration;

namespace Libreria
{
    public class ConfigDataAccess
    {
        private static readonly bool optional = false;
        private static readonly bool reload = true;
        private static readonly IConfigurationBuilder Builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional, reload);
        protected static readonly IConfigurationRoot Configuration = Builder.Build();
    }
}