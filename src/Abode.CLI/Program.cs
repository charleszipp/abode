﻿using Azure;
using Azure.DigitalTwins.Core;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Abode
{
    class Program
    {
        static async Task Main()
        {
            IDigitalTwinsClient client = new AzureDigitalTwinsClient();

            // Upload the models to the service
            Console.WriteLine();
            Console.WriteLine($"Upload the Room model");
            string roomDtdl = File.ReadAllText("models/Room.json");
            Model roomModel = new(client);
            await roomModel.UploadModel(roomDtdl);

            Console.WriteLine($"Upload the Thermostat model");
            string thermostatDtdl = File.ReadAllText("models/Thermostat.json");
            Model thermostatModel = new(client);
            await thermostatModel.UploadModel(thermostatDtdl);

            // Read a list of models back from the service
            AsyncPageable<DigitalTwinsModelData> modelDataList = client.GetModelsAsync();
            await foreach (DigitalTwinsModelData md in modelDataList)
            {
                Console.WriteLine($"Model: {md.Id}");
            }
            
            // Create twins
            var roomTwinData = new BasicDigitalTwin();
            roomTwinData.Metadata.ModelId = "dtmi:abode:Room;1";
            roomTwinData.Contents.Add("data", "kitchen");

            var thermostatTwinData = new BasicDigitalTwin();
            thermostatTwinData.Metadata.ModelId = "dtmi:abode:Thermostat;1";
            thermostatTwinData.Contents.Add("Temperature", 69.01);

            try
            {
                roomTwinData.Id = "roomTwin-0";
                await client.CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(roomTwinData.Id, roomTwinData);
                Log.Ok($"Created Room twin: {roomTwinData.Id}");

                thermostatTwinData.Id = "thermostatTwin-0";
                await client.CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(thermostatTwinData.Id, thermostatTwinData);
                Log.Ok($"Created Thermostat twin: {thermostatTwinData.Id}");

                // Connect the twins with a relationship
                await client.CreateRelationshipAsync("roomTwin-0", "thermostatTwin-0");
            }
            catch (RequestFailedException e)
            {
                Log.Error($"Create twin error: {e.Status}: {e.Message}");
            }

            // List the relationships
            await client.ListRelationshipsAsync("roomTwin-0");
        }
    }
}
