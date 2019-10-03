using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace survey.data
{
    public class Client : IClient, IPublicKeyId
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

        [JsonProperty("billingId")]
        public int? BillingId { get; set; }

        [JsonProperty("publicKey")]
        public Guid PublicKey { get; set; }

        [JsonProperty("workspaces")]
        public ICollection<Workspace> Workspaces { get; } = new List<Workspace>();
    }
}
