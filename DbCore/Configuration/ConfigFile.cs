using Microsoft.Extensions.Configuration;
using Serilog;
using System.Reflection;

namespace DbCore.Configuration
{
    public static class ConfigFile
    {
        public static IConfiguration GetConfiguration()
        {
            string fileName = "appsettings.json";
            try
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory() + "/Settings/")
                    .AddJsonFile(fileName, optional: true, reloadOnChange: false);
                Log.Information("Список переменных тестовой среды успешно сформирован");
                IConfiguration config = builder.Build();

                return config;
            }
            catch (Exception ex)
            {
                Log.Error($"Не удалось сформировать список переменных для тестовой среды\n{ex.Message}");
                throw;
            }
        }

        private static IConfiguration BuildConfiguration()
        {
            var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath ?? throw new InvalidOperationException())
                .AddJsonFile("appsettings.json");
            var appSettingFiles = Directory.EnumerateFiles(basePath ?? string.Empty, "appsettings.*.json");
            foreach (var appSettingFile in appSettingFiles)
            {
                builder.AddJsonFile(appSettingFile);
            }

            return builder.Build();
        }
    }
}