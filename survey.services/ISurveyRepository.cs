using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using survey.data;

namespace survey.services
{
    public interface ISurveyRepository
    {
        IEnumerable<Client> GetClients();
        Task<Client> GetClientAsync(int Id);
        Task<Client> GetClientAsync(Guid PublicKey);

        IEnumerable<Workspace> GetWorkspaces();
        Task<Workspace> GetWorkspaceAsync(int Id);
        Task<Workspace> GetWorkspaceAsync(Guid PublicKey);

        IEnumerable<Survey> GetSurveys();
        Task<Survey> GetSurveyAsync(int Id);
        Task<Survey> GetSurveyAsync(string Name);
        Task<Survey> GetSurveyAsync(Guid PublicKey);
        Task<IEnumerable<Survey>> GetSurveysToday();
        Task<int> GetSurveysTodayCount();
        Task<Survey> CreateSurveyAsync(string SurveyName, IEnumerable<Question> Questions);
        Task<Survey> CreateSurveyAsync(string SurveyName, IEnumerable<QuestionRequest> QuestionRequests);

        IEnumerable<Question> GetQuestions();
        Task<Question> GetQuestionAsync(int Id);
        Task<Question> GetQuestionAsync(Guid PublicKey);
        Task<IEnumerable<SurveyQuestion>> GetSurveyQuestionsAsync(int SurveyId);

        IEnumerable<QuestionType> GetQuestionTypes();
        Task<QuestionType> GetQuestionTypeAsync(string Name);

        IEnumerable<Response> GetResponses();
        IEnumerable<IResponse> GetResponsesBySurveyId(int SurveyId);
        Task<IEnumerable<Response>> GetResponsesBySurveyIdAsync(int SurveyId);
        Task<IEnumerable<object>> GetResponsesCustomBySurveyIdAsync(int SurveyId);
        Task<int> GetResponsesCountAsync(int SurveyId);
        Task<object> GetResponsesStatsAsync(int SurveyId);
        Task<IResponse> UpdateResponseAsync(int SurveyId, int QuestionId, string Answer);
        void UpdateResponse(int SurveyId, int QuestionId, string Answer);

        bool EntityChanged<T>(T Entity) where T : class;
        Task<T> FindEntityAsync<T>(int Id) where T : class;
        Task<T> FindEntityAsync<T>(Guid PublicKey) where T : class;
        Task<T> FindEntityAsync<T>(object PulicKeyId) where T : class;
        Task<Guid> FindEntityPublicKeyAsync<T>(int Id) where T : class;
        object FindEntityAsync(Guid PublicKey);
    }
}
