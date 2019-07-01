using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using survey.data;

namespace survey.services
{
    public class SurveyRepository : ISurveyRepository
    {
        public readonly ILogger log;
        public readonly IMemoryCache cache;

        public readonly SurveyContext context;

        public SurveyRepository(ILogger<SurveyRepository> log, IMemoryCache cache, SurveyContext context)
        {
            this.log = log;
            this.cache = cache;

            this.context = context;
        }

        /*
         * Survey
         */
        public IEnumerable<ISurvey> GetSurveys()
        {
            return GetSurveysQuery()
                .AsEnumerable();
        }
        public async Task<ISurvey> GetSurvey(int Id)
        {
            return await GetSurveysQuery()
                .FirstOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<ISurvey> GetSurvey(Guid PublicKey)
        {
            return await GetSurveysQuery()
                .FirstOrDefaultAsync(x => x.PublicKey == PublicKey);
        }
        private IQueryable<Survey> GetSurveysQuery()
        {
            return context.Surveys
                .AsQueryable();
        }

        /*
         * Survey Question
         */
        public IEnumerable<ISurveyQuestion> GetSurveyQuestions()
        {
            return context.SurveyQuestions
                .Include(x => x.Survey)
                .Include(x => x.Question)
                    .ThenInclude(x => x.Type)
                .GroupBy(x => x.Survey)
                .SelectMany(x => x)
                .ToList();
        }
        public IEnumerable<ISurveyQuestion> GetSurveyQuestionsBySurveyId(int SurveyId)
        {
            return GetSurveyQuestions()
                .Where(x => x.SurveyId == SurveyId);
        }
        public IEnumerable<ISurveyQuestion> GetSurveyQuestionsByQuestionId(int QuestionId)
        {
            return GetSurveyQuestions()
                .Where(x => x.QuestionId == QuestionId);
        }
        private IQueryable<SurveyQuestion> GetSurveyQuestionsQuery()
        {
            return context.SurveyQuestions
                .AsQueryable();
        }


        /*
         * Response
         */
        public IEnumerable<IResponse> GetResponses()
        {
            return GetResponsesQuery()
                .AsEnumerable();
        }
        public async Task<IResponse> UpdateResponse(int SurveyId, int QuestionId, string Asnwer)
        {
            var response = GetResponsesQuery()
                .FirstOrDefault(x => x.SurveyQuestion.SurveyId == SurveyId &&
                    x.SurveyQuestion.QuestionId == QuestionId &&
                    x.QuestionTypeAnswer.Answer == Asnwer);

            response.Count++;

            await context.SaveChangesAsync();

            return response;
        }
        private IQueryable<Response> GetResponsesQuery()
        {
            return context.Responses
                    .Include(x => x.SurveyQuestion)
                        .ThenInclude(x => x.Survey)
                    .Include(x => x.SurveyQuestion)
                        .ThenInclude(x => x.Question)
                    .Include(x => x.QuestionTypeAnswer)
                .AsQueryable();
        }
    }
}
