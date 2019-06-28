using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace survey.data
{
    public class Question : IQuestion<Question>, IPublicKeyId
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("public_key")]
        public Guid PublicKey { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("question_text")]
        public string Text { get; set; }

        [JsonProperty("responses")]
        public ICollection<Response> Responses { get; set; } = new List<Response>();
    }
}
