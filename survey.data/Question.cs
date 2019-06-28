using System;

using Newtonsoft.Json;

namespace survey.data
{
    public class Question
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("type")]
        public Type Type { get; set; }
    }

    public struct Type
    {
        public int OpenEnded;
    }
}
