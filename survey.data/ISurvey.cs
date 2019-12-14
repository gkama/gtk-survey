using System;
using System.Collections.Generic;
using System.Text;

namespace survey.data
{
    public interface ISurvey
    {
        int Id { get; set; }
        string Name { get; set; }
        DateTime Created { get; set; }
        string CreatedBy { get; set; }
        DateTime LastUpdated { get; set; }
        string LastUpdatedBy { get; set; }
        int? CategoryId { get; set; }
        SurveyCategory Category { get; set; }
        int? WorkspaceId { get; set; }
        Workspace Workspace { get; set; }
        Guid PublicKey { get; set; }
        ICollection<SurveyQuestion> SurveyQuestions { get; }
    }
}
