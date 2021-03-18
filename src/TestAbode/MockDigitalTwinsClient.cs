using Azure;
using Azure.DigitalTwins.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abode;

namespace TestAbode
{
    class MockDigitalTwinsClient : IDigitalTwinsClient
    {
        List<string> _models;

        public MockDigitalTwinsClient() {
            _models = new List<string>();
        }

        public async Task<string> CreateModel(string models)
        {
            string response;
            if (await CheckIfModelExist(models))
            {
                response = "Cannot create Model, already exists.";
            }
            else
            {
                _models.Add(models);
                response = "Model created.";
            }
            return response;
        }

        public Task<bool> CheckIfModelExist(string dtdlId)
        {
            if (_models.Contains(dtdlId))
            {
                return Task.FromResult(true);
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
}
