using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace survey.data
{
    public class SurveyCategory : IPublicKeyId, INameId
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nameId")]
        public string NameId => this.Name.RemoveWhitespaceToLower();

        [JsonProperty("publicKey")]
        public Guid PublicKey { get; set; }
    }
}
