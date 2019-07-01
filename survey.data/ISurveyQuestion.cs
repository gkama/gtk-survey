using System;
using System.Collections.Generic;
using System.Text;

namespace survey.data
{
    public interface ISurveyQuestion
    {
        int SurveyId { get; set; }
        Survey Survey { get; set; }
        int QuestionId { get; set; }
        Question Question { get; set; }
    }
}
