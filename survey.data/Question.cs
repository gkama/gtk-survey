using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace survey.data
{
    public class Question
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("type")]
        public Type Type { get; set; }

        [JsonProperty("responses")]
        public IList<object> Responses { get; set; } = new List<object>();
    }

    public struct Type
    {
        public int OpenEnded;
        public int MultipleChoice;
        public int YesNo;
    }
}
