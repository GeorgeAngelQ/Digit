using Microsoft.Extensions.Configuration;

namespace Libreria
{
    public class ConfigDataAccess
    {
        private static IConfigurationRoot? _configuration;

        public static void SetConfiguration(IConfiguration configuration)
        {
            _configuration = (IConfigurationRoot)configuration;
        }

        protected static IConfigurationRoot Configuration
        {
            get
            {
                if (_configuration == null)
                    throw new InvalidOperationException("No se ha inicializado la propiedad ConnectionString. Usa SetConfiguration en Program.cs.");
                return _configuration;
            }
        }
    }
}
