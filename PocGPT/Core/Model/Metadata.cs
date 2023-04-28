using Newtonsoft.Json;

namespace PocGPT.Core.Model
{
    public class Metadata
    {
        [JsonProperty("#messageEmitter")]
        public string? MessageEmitter { get; set; }

        [JsonProperty("uber-trace-id")]
        public string? UberTraceId { get; set; }

        [JsonProperty("#uniqueId")]
        public string? UniqueId { get; set; }

        [JsonProperty("date_created")]
        public string? DateCreated { get; set; }

        [JsonProperty("#date_processed")]
        public string? DateProcessed { get; set; }

        [JsonProperty("$originator")]
        public string? Originator { get; set; }

        [JsonProperty("$claims")]
        public string? Claims { get; set; }

        [JsonProperty("$internalId")]
        public string? InternalId { get; set; }

        [JsonProperty("$originatorSessionRemoteNode")]
        public string? OriginatorSessionRemoteNode { get; set; }

        [JsonProperty("#messageKind")]
        public string? MessageKind { get; set; }

        [JsonProperty("$elapsedTimeToStorage")]
        public string? ElapsedTimeToStorage { get; set; }

    }
}
