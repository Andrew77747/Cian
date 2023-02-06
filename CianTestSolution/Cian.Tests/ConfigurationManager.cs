using Infrastructure.Settings;
using Microsoft.Extensions.Configuration;

namespace Cian.Tests
{
    public class ConfigurationManager
    {
        public Appsettings GetSettings()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json").Build();

            var settings = config.Get<Appsettings>();

            return settings;
        }
    }
}