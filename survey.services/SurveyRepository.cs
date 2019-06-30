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

        public IEnumerable<ISurvey> GetSurveys()
        {
            return GetSurveysQuery()
                .AsEnumerable();           
        }

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

        public IQueryable<Survey> GetSurveysQuery()
        {
            return context.Surveys
                .AsQueryable();
        }

        public IEnumerable<object> GetSurveyQuestions()
        {
            return context.SurveyQuestions
                .Include(x => x.Survey)
                .Include(x => x.Question)
                    .ThenInclude(x => x.Type)
                .GroupBy(x => x.SurveyId)
                .ToList();
        }

        public IQueryable<Response> GetResponsesQuery()
        {
            return context.Responses
                    .Include(x => x.Question)
                        .ThenInclude(x => x.Type)
                            .ThenInclude(x => x.Answers)
                .AsQueryable();
        }
    }
}
