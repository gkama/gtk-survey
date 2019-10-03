using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace survey.data
{
    public class Workspace : IWorkspace, IPublicKeyId
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("lastUpdated")]
        public DateTime LastUpdated { get; set; }

        [JsonProperty("clientId")]
        public int? ClientId { get; set; }

        [JsonProperty("client")]
        public Client Client { get; set; }

        [JsonProperty("publicKey")]
        public Guid PublicKey { get; set; }

        [JsonProperty("surveys")]
        public ICollection<Survey> Surveys { get; } = new List<Survey>();
    }
}
