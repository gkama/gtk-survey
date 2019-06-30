using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace survey.data
{
    public class SurveyQuestion : IPublicKeyId
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("public_key")]
        public Guid PublicKey { get; set; }

        [JsonProperty("survey_id")]
        public int SurveyId { get; set; }

        [JsonProperty("survey")]
        public Survey Survey { get; set; }

        [JsonProperty("question_id")]
        public int QuestionId { get; set; }

        [JsonProperty("question")]
        public Question Question { get; set; }
    }
}
