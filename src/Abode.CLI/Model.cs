using Azure;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Abode
{
    public class Model
    {
        readonly IAbode _abode;
        public Model(IAbode abode)
        {
            _abode = abode;
        }
        public async Task<string> UploadModel(string dtdl)
        {
            string response;
            string dtdlId = JObject.Parse(dtdl)["@id"].Value<string>();

            if (await _abode.CheckIfModelExist(dtdlId))
            {
                Log.Error($"Model: {dtdlId} already exists");
                response = "Model already exists.";
                return response;
            }
            try
            {
                response = await _abode.CreateModel(dtdl);
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
