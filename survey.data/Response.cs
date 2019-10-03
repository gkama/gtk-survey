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

        [JsonProperty("surveyQuestionId")]
        public int SurveyQuestionId { get; set; }

        [JsonProperty("surveyQuestion")]
        public SurveyQuestion SurveyQuestion { get; set; }

        [JsonProperty("questionTypeAnswerId")]
        public int QuestionTypeAnswerId { get; set; }

        [JsonProperty("questionTypeAnswer")]
        public QuestionTypeAnswer QuestionTypeAnswer { get; set; }

        [JsonProperty("publicKey")]
        public Guid PublicKey { get; set; }
    }
}
