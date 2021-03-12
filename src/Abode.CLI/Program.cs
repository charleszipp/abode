using Azure.DigitalTwins.Core;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Abode.CLI
{
    class Program
    {
        static async Task Main()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();
            Uri adtInstanceUrl;
            try
            {
                // Read configuration data from the 
                IConfiguration config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                    .Build();
                adtInstanceUrl = new Uri(config["instanceUrl"]);
            }
            catch (Exception ex) when (ex is FileNotFoundException || ex is UriFormatException)
            {
                Log.Error($"Could not read configuration. Have you configured your ADT instance URL in appsettings.json?\n\nException message: {ex.Message}");
                return;
            }

            Log.Information("Authenticating...");

            var creds = new DefaultAzureCredential();
            var client = new DigitalTwinsClient(adtInstanceUrl, creds);

            Log.Information("Service client created - ready!");
        }
    }
}
