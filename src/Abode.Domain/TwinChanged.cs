using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using Azure.DigitalTwins.Core;

namespace Abode.Domain
{
    public class TwinChanged
    {
        [JsonPropertyName("$modelId")]
        public string ModelId { get; set; }
        [JsonPropertyName("patch")]
        public TwinPatch[] Patch { get; set; }

    }
    public class TwinPatch
    {
        [JsonPropertyName("path")]
        public string Path { get; set; }
        [JsonPropertyName("op")]
        public TwinPatchOperation Operation { get; set; }
        [JsonPropertyName("value")]
        public object Value { get; set; }
    }

    [DataContract(Name = "op")]
    public enum TwinPatchOperation
    {
        [DataMember(Name = "replace")]
        Replace,
        [DataMember(Name = "add")]
        Add
    }
}
