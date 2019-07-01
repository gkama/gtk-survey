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

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("survey_question_id")]
        public int SurveyQuestionId { get; set; }

        [JsonProperty("survey_question")]
        public SurveyQuestion SurveyQuestion { get; set; }

        [JsonProperty("question_type_answer_id")]
        public int QuestionTypeAnswerId { get; set; }

        [JsonProperty("question_type_answer")]
        public QuestionTypeAnswer QuestionTypeAnswer { get; set; }

        [JsonProperty("public_key")]
        public Guid PublicKey { get; set; }
    }
}
