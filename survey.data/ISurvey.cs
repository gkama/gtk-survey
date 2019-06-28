using System;
using System.Collections.Generic;
using System.Text;

namespace survey.data
{
    public interface ISurvey
    {
        int Id { get; set; }
        string Name { get; set; }
        Guid PublicKey { get; set; }
        ICollection<Question> Questions { get; set; }
    }
}
