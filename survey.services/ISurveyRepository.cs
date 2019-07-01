using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using survey.data;

namespace survey.services
{
    public interface ISurveyRepository
    {
        IEnumerable<ISurvey> GetSurveys();
        IEnumerable<ISurveyQuestion> GetSurveyQuestions();
        IEnumerable<IResponse> GetResponses();
        Task<IResponse> UpdateResponse(int SurveyId, int QuestionId, string Asnwer);
    }
}
