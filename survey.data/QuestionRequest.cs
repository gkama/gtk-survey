using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace survey.data
{
    public class QuestionRequest
    {
        [JsonProperty("question_name")]
        public string QuestionName { get; set; }

        [JsonProperty("question_text")]
        public string QuestionText { get; set; }

        [JsonProperty("question_type_name")]
        public string QuestionTypeName { get; set; }

        [JsonProperty("question_type_answers")]
        public IEnumerable<string> QuestionTypeAnswers { get; set; } = new List<string>();
    }

    public class SurveyQuestionRequest
    {
        [JsonProperty("survey_name")]
        public string SurveyName { get; set; }

        [JsonProperty("question_requests")]
        public IEnumerable<QuestionRequest> QuestionRequests { get; set; } = new List<QuestionRequest>();
    }
}
