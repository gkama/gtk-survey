using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace survey.data
{
    public class Response : IResponse, IPublicKeyId
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("question_id")]
        public int QuestionId { get; set; }

        [JsonProperty("public_key")]
        public Guid PublicKey { get; set; }

        [JsonProperty("response_text")]
        public string ResponseText { get; set; }
    }
}
