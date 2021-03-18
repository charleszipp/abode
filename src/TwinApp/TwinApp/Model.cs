using Azure;
using Azure.DigitalTwins.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace TwinApp
{
    public class Model
    {
        readonly ITwin _twin;
        public Model(ITwin twin)
        {
            _twin = twin;
        }
        public async Task<string> UploadModel(string dtdl)
        {
            string response;
            string dtdlId = JObject.Parse(dtdl)["@id"].Value<string>();

            if (await _twin.CheckIfModelExist(dtdlId))
            {
                Log.Error($"Model: {dtdlId} already exists");
                response = "Model already exists.";
                return response;
            }

            try
            {
                response = await _twin.CreateModel(dtdl);
            }
            catch (RequestFailedException e)
            {
                Console.WriteLine($"Upload model error: {e.Status}: {e.Message}");
                response = "Upload model error.";
            }
            return response;
        }        
    }
}
