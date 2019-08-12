using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using survey.data;

namespace survey.services
{
    public interface ISurveyRepository
    {
        IEnumerable<Survey> GetSurveys();
        Task<Survey> GetSurveyAsync(int Id);
        Task<Survey> GetSurveyAsync(string Name);
        Task<Survey> GetSurveyAsync(Guid PublicKey);
        Task<IEnumerable<Survey>> GetSurveysToday();
        Task<int> GetSurveysTodayCount();
        Task<IEnumerable<SurveyQuestion>> CreateSurveyAsync(string SurveyName, IEnumerable<Question> Questions);
        Task<IEnumerable<SurveyQuestion>> CreateSurveyAsync(string SurveyName, IEnumerable<QuestionRequest> QuestionRequests);
        Task<Question> GetQuestionAsync(int Id);
        Task<Question> GetQuestionAsync(Guid PublicKey);
        IEnumerable<SurveyQuestion> GetSurveyQuestions();
        Task<IEnumerable<SurveyQuestion>> GetSurveyQuestions(int SurveyId);
        Task<QuestionType> GetQuestionTypeAsync(string Name);
        IEnumerable<IResponse> GetResponses();
        IEnumerable<IResponse> GetResponsesBySurveyId(int SurveyId);
        Task<IEnumerable<object>> GetResponsesCustomBySurveyIdAsync(int SurveyId);
        Task<int> GetResponsesCountAsync(int SurveyId);
        Task<object> GetResponsesStatsAsync(int SurveyId);
        Task<IResponse> UpdateResponseAsync(int SurveyId, int QuestionId, string Asnwer);

        bool EntityChanged<T>(T Entity) where T : class;
    }
}
