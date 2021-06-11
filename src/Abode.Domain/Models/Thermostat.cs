using Azure;
using Azure.DigitalTwins.Core;
using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Abode.Domain.Models
{
    public class Thermostat : BasicDigitalTwin
    {
        public const string ModelId = "dtmi:abode:thermostat;2";        

        public Thermostat()
        {
            this.Metadata.ModelId = ModelId;
        }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("temperature")]
        public double Temperature { get; set; }

        [JsonPropertyName("humidity")]
        public double Humidity { get; set; }
    }
}
