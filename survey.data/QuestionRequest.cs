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
}
