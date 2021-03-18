using Abode;
using Azure;
using Azure.DigitalTwins.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestAbode
{
    class MockDigitalTwinsClient : IDigitalTwinsClient
    {
        List<MockModelData> _models;

        public MockDigitalTwinsClient() {
            _models = new List<MockModelData>();
        }

        public async Task<string> CreateModel(string models)
        {
            MockModelData modelData = new MockModelData(models);
            _models.Add(modelData);
            return "Model created.";
        }

        public Task<bool> CheckModelExists(string dtdlId)
        {
            foreach (MockModelData modelData in _models)
            {
                if (modelData.Id == dtdlId)
                {
                    return Task.FromResult(true);
                }
            }
            return Task.FromResult(false); 
        }

        public Task<Response<DigitalTwinsModelData[]>> CreateModelsAsync(IEnumerable<string> models)
        {
            throw new NotImplementedException();
        }

        public AsyncPageable<DigitalTwinsModelData> GetModelsAsync()
        {
            throw new NotImplementedException();
        }

        public Task CreateRelationshipAsync(string srcId, string targetId)
        {
            throw new NotImplementedException();
        }

        public Task ListRelationshipsAsync(string srcId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<BasicDigitalTwin>> CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(string twinId, BasicDigitalTwin twinData)
        {
            throw new NotImplementedException();
        }
    }

    public class MockModelData
    {
        public string Id { get; set; }
    
        public string Data { get; set; }

        public MockModelData(string dtdl)
        {
            Id = JObject.Parse(dtdl)["@id"].Value<string>();
            Data = dtdl;
        }
    }
}
