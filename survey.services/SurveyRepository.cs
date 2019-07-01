using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using survey.data;

namespace survey.services
{
    public class SurveyRepository : ISurveyRepository
    {
        public readonly ILogger log;

        public readonly SurveyContext context;

        public SurveyRepository(ILogger<SurveyRepository> log, SurveyContext context)
        {
            this.log = log;

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

        public IEnumerable<object> GetResponses2()
        {
            var _responses = new List<object>();
            var responses = context.Responses.AsEnumerable();

            foreach (var r in responses)
            {
                var answer = r.Question
                    .Type
                    .Answers
                    .FirstOrDefault(x => x.Id == r.Id);
            }

            return _responses.ToList();
        }


        private IQueryable<Response> GetResponsesQuery()
        {
            return context.Responses
                    .Include(x => x.Question)
                        .ThenInclude(x => x.Type)
                            .ThenInclude(x => x.Answers)
                .AsQueryable();
        }
    }
}
