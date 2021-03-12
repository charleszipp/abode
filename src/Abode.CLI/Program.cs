using Azure.DigitalTwins.Core;
using Azure.Identity;
using System;

namespace Abode.CLI
{
    class Program
    {
        private const string _adtHostName = "adt-leo-dev-usea-czp.api.eus.digitaltwins.azure.net";

        static void Main(string[] args)
        {
            var creds = new DefaultAzureCredential();
            var endpoint = new Uri($"https://{_adtHostName}");
            var client = new DigitalTwinsClient(endpoint, creds);
        }
    }

    public class House
    {
        public int SquareFeet { get; set; }
    }
}
