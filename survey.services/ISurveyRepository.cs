using System;
using System.Collections.Generic;
using System.Text;

using survey.data;

namespace survey.services
{
    public interface ISurveyRepository
    {
        IEnumerable<IQuestion<Question>> GetQuestions();
    }
}
