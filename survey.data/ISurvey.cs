using System;
using System.Collections.Generic;
using System.Text;

namespace survey.data
{
    public interface ISurvey
    {
        int Id { get; set; }
        string Name { get; set; }
        DateTime CreationDate { get; set; }
        string CreatedBy { get; set; }
        DateTime LastUpdated { get; set; }
        string LastUpdatedBy { get; set; }
        Guid PublicKey { get; set; }
    }
}
