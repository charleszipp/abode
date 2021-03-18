using Azure;
using Azure.DigitalTwins.Core;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TwinApp
{
    class Program
    {
        static async Task Main()
        {
            ITwin twin = new Twin();

            // Upload the models to the service
            Console.WriteLine();
            Console.WriteLine($"Upload a yard model");
            string dtdlYard = File.ReadAllText("Models/Yard.json");
            Model yardModel = new(twin);
            await yardModel.UploadModel(dtdlYard);

            Console.WriteLine($"Upload a camera model");
            string dtdlCamera = File.ReadAllText("Models/Camera.json");
            Model cameraModel = new(twin);
            await cameraModel.UploadModel(dtdlCamera);

            // Read a list of models back from the service
            AsyncPageable<DigitalTwinsModelData> modelDataList = twin.GetModelsAsync();
            await foreach (DigitalTwinsModelData md in modelDataList)
            {
                Console.WriteLine($"Model: {md.Id}");
            }
            
            // Create twins
            var yardTwinData = new BasicDigitalTwin();
            yardTwinData.Metadata.ModelId = "dtmi:example:Yard;1";
            yardTwinData.Contents.Add("data", $"front yard");

            var cameraTwinData = new BasicDigitalTwin();
            cameraTwinData.Metadata.ModelId = "dtmi:example:Camera;1";
            cameraTwinData.Contents.Add("Light", false);

            try
            {
                yardTwinData.Id = $"yardTwin-0";
                await twin.CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(yardTwinData.Id, yardTwinData);
                Console.WriteLine($"Created yard twin: {yardTwinData.Id}");

                cameraTwinData.Id = $"cameraTwin-0";
                await twin.CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(cameraTwinData.Id, cameraTwinData);
                Console.WriteLine($"Created camera twin: {cameraTwinData.Id}");

                // Connect the twins with a relationship
                await twin.CreateRelationshipAsync($"yardTwin-0", $"cameraTwin-0");
            }
            catch (RequestFailedException e)
            {
                Console.WriteLine($"Create twin error: {e.Status}: {e.Message}");
            }

            // List the relationships
            await twin.ListRelationshipsAsync("yardTwin-0");
        }
    }
}
