using Azure;
using Azure.DigitalTwins.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abode
{
    public interface IDigitalTwinsClient
    {
        Task<string> CreateModel(string models);

        Task<bool> CheckModelExists(string dtdlId);
 
        Task<Response<DigitalTwinsModelData[]>> CreateModelsAsync(IEnumerable<string> models);

        AsyncPageable<DigitalTwinsModelData> GetModelsAsync();

        Task CreateRelationshipAsync(string srcId, string targetId);

        Task ListRelationshipsAsync(string srcId);

        Task<Response<BasicDigitalTwin>> CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(string twinId, BasicDigitalTwin twinData);
    }
}
