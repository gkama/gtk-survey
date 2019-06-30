using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace survey.data
{
    public class SurveyQuestion
    {
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
