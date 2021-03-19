﻿using Azure;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Abode
{
    public class Model
    {
        readonly IDigitalTwinsClient _client;
        public Model(IDigitalTwinsClient client)
        {
            _client = client;
        }
        public async Task<string> UploadModel(string dtdl)
        {
            string response;
            string dtdlId = JObject.Parse(dtdl)["@id"].Value<string>();

            if (await _client.CheckModelExists(dtdlId))
            {
                Log.Error($"Model: {dtdlId} already exists");
                response = "Model cannot be uploaded as it already exists.";
                return response;
            }
            try
            {
                response = await _client.CreateModel(dtdl);
            }
            catch (RequestFailedException e)
            {
                Log.Error($"Upload model error: {e.Status}: {e.Message}");
                response = "Upload model error.";
            }
            return response;
        }        
    }
}