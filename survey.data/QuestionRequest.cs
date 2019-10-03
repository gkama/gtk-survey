using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace survey.data
{
    public class QuestionRequest
    {
        [JsonProperty("questionName")]
        public string QuestionName { get; set; }

        [JsonProperty("questionText")]
        public string QuestionText { get; set; }

        [JsonProperty("questionTypeName")]
        public string QuestionTypeName { get; set; }

        [JsonProperty("questionTypeAnswers")]
        public IEnumerable<string> QuestionTypeAnswers { get; set; } = new List<string>();
    }

    public class SurveyQuestionRequest
    {
        [JsonProperty("surveyName")]
        public string SurveyName { get; set; }

        [JsonProperty("questionRequests")]
        public IEnumerable<QuestionRequest> QuestionRequests { get; set; } = new List<QuestionRequest>();
    }
}
