﻿using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace survey.data
{
    /*
     * Question
     * 
     * The approach to questions in a survey would be the following:
     *  a question is based off of its type
     *  a type has a list of possible answers, unless it's open ended
     *  a question type answer is stored for each question type and therefore for each question
     */
    public class Question : IQuestion<IQuestionType>, IPublicKeyId
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("questionText")]
        public string Text { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("lastUpdated")]
        public DateTime LastUpdated { get; set; }

        [JsonProperty("typeId")]
        public int TypeId { get; set; }

        [JsonProperty("type")]
        public QuestionType Type { get; set; }

        [JsonProperty("publicKey")]
        public Guid PublicKey { get; set; }
    }
}
