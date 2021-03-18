using Azure;
using Azure.DigitalTwins.Core;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Abode
{
    public class AzureDigitalTwinsClient : IDigitalTwinsClient
    {
        readonly DigitalTwinsClient _client;
        readonly Uri adtInstanceUrl;

        public AzureDigitalTwinsClient()
        {
            try
            {
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

            Log.Ok("Authenticating...");
            var credential = new DefaultAzureCredential();
            _client = new DigitalTwinsClient(adtInstanceUrl, credential);

            Log.Ok($"Service client created – ready to go");
        }

        public async Task<bool> CheckModelExists(string dtdlId)
        {
            AsyncPageable<DigitalTwinsModelData> modelDataList = GetModelsAsync();
            await foreach (DigitalTwinsModelData modelData in modelDataList)
            {
                if (dtdlId == modelData.Id)
                {
                    return true;
                }
            }
            return false;
        }
        
        public async Task<string> CreateModel(string modelDtdll)
        {
            Response<DigitalTwinsModelData[]> res = await CreateModelsAsync(new List<string> { modelDtdll });
            return res.Value.ToString();
        }

        public async Task<Response<DigitalTwinsModelData[]>> CreateModelsAsync(IEnumerable<string> models)
        {
            return await _client.CreateModelsAsync(models);
        }

        public AsyncPageable<DigitalTwinsModelData> GetModelsAsync()
        {
            return _client.GetModelsAsync();
        }

        public async Task<Response<BasicDigitalTwin>> CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(string twinId, BasicDigitalTwin twinData)
        {
            return await _client.CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(twinId, twinData);
        }

        public async Task CreateRelationshipAsync(string srcId, string targetId)
        {
            var relationship = new BasicRelationship
            {
                TargetId = targetId,
                Name = "contains"
            };

            try
            {
                string relId = $"{srcId}-contains->{targetId}";
                await _client.CreateOrReplaceRelationshipAsync(srcId, relId, relationship);
                Log.Ok("Created relationship successfully");
            }
            catch (RequestFailedException e)
            {
                Log.Error($"Create relationship error: {e.Status}: {e.Message}");
            }
        }

        public async Task ListRelationshipsAsync(string srcId)
        {
            try
            {
                AsyncPageable<BasicRelationship> results = _client.GetRelationshipsAsync<BasicRelationship>(srcId);
                Console.WriteLine($"Twin {srcId} is connected to:");
                await foreach (BasicRelationship rel in results)
                {
                    Console.WriteLine($" -{rel.Name}->{rel.TargetId}");
                }
            }
            catch (RequestFailedException e)
            {
                Log.Error($"Relationship retrieval error: {e.Status}: {e.Message}");
            }
        }
    }
}
