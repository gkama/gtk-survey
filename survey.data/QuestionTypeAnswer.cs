using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace survey.data
{
    public class QuestionTypeAnswer : IPublicKeyId
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("question_type_id")]
        public int TypeId { get; set; }

        [JsonProperty("answer")]
        public string Answer { get; set; }

        [JsonProperty("public_key")]
        public Guid PublicKey { get; set; }
    }
}
