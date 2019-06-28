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

        public IEnumerable<IQuestion<Question>> GetQuestions()
        {
            return GetQuestionsQuery()
                .AsEnumerable();           
        }

        public IQueryable<Question> GetQuestionsQuery()
        {
            return context
                .Questions
                    .Include(x => x.Responses)
                .AsQueryable();
        }
    }
}
