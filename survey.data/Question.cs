using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace survey.data
{
    public class Question : IQuestion<Question>
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

    public struct QuestionType
    {
        public int OpenEnded;       //Open ended answers
        public int MultipleChoice;  //Multiple choice defined by the user
        public int YesNo;           //Yes or No
        public int Semantic;        //On a scale of 1 to 10 (max is 1 to 10)
        public int Likert;          //Strongly agree, Agree, Neither agree nor disagree, Disagree, Strongly disagree
    }
}
