using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace survey.data
{
    public class Question : IQuestion<IQuestionType>, IPublicKeyId
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("type_id")]
        public int TypeId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("question_text")]
        public string Text { get; set; }

        [JsonProperty("public_key")]
        public Guid PublicKey { get; set; }


        [JsonProperty("type")]
        public QuestionType Type { get; set; }

        [JsonProperty("responses")]
        public ICollection<Response> Responses { get; set; } = new List<Response>();
    }
}
