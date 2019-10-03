using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace survey.data
{
    public class SurveyQuestion : ISurveyQuestion, IPublicKeyId
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("surveyId")]
        public int SurveyId { get; set; }

        [JsonProperty("survey")]
        public Survey Survey { get; set; }

        [JsonProperty("questionId")]
        public int QuestionId { get; set; }

        [JsonProperty("question")]
        public Question Question { get; set; }

        [JsonProperty("publicKey")]
        public Guid PublicKey { get; set; }
    }
}
