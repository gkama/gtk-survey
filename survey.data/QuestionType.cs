using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace survey.data
{
    //public int OpenEnded;       //Open ended answers
    //public int MultipleChoice;  //Multiple choice defined by the user
    //public int YesNo;           //Yes or No
    //public int Semantic;        //On a scale of 1 to 10 (max is 1 to 10)
    //public int Likert;          //Strongly agree, Agree, Neither agree nor disagree, Disagree, Strongly disagree

    public class QuestionType : IQuestionType, IPublicKeyId
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("public_key")]
        public Guid PublicKey { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
