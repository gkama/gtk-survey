using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace survey.data
{
    public class Response : IResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("public_key")]
        public Guid PublicKey { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
