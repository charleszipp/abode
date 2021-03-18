using Azure;
using Azure.DigitalTwins.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwinApp;

namespace TwinAppTest
{
    class MockTwin : ITwin
    {
        int count = 0;
        public MockTwin() { }

        public async Task<string> CreateModel(string models)
        {
            string response;
            if (count > 0)
            {
                response = "Cannot create Model, already exists.";
            }
            else
            {
                response = "Model created.";
            }
            count++;
            return response;
        }

        public async Task<bool> CheckIfModelExist(string dtdlId)
        {
            await Task.Delay(0);
            if (count > 0)
            {
                return true;
            }
                
            return false;
        }

        public async Task<Response<DigitalTwinsModelData[]>> CreateModelsAsync(IEnumerable<string> models)
        {
            throw new NotImplementedException();
        }
        public AsyncPageable<DigitalTwinsModelData> GetModelsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task CreateRelationshipAsync(string srcId, string targetId)
        {
            throw new NotImplementedException();
        }

        public async Task ListRelationshipsAsync(string srcId)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<BasicDigitalTwin>> CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(string twinId, BasicDigitalTwin twinData)
        {
            throw new NotImplementedException();
        }
    }
}
