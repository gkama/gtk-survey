using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace survey.data
{
    public class Survey : ISurvey, IPublicKeyId
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("createdBy")]
        public string CreatedBy { get; set; }

        [JsonProperty("lastUpdated")]
        public DateTime LastUpdated { get; set; }

        [JsonProperty("lastUpdatedBy")]
        public string LastUpdatedBy { get; set; }

        [JsonProperty("categoryId")]
        public int? CategoryId { get; set; }

        [JsonProperty("category")]
        public SurveyCategory Category { get; set; }

        [JsonProperty("workspaceId")]
        public int? WorkspaceId { get; set; }

        [JsonProperty("workspace")]
        public Workspace Workspace { get; set; }

        [JsonProperty("publicKey")]
        public Guid PublicKey { get; set; }

        [JsonProperty("surveyQuestions")]
        public ICollection<SurveyQuestion> SurveyQuestions { get; } = new List<SurveyQuestion>();
    }
}
