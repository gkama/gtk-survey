using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Logging;

namespace survey.services
{
    public class SurveyCalculations : ISurveyCalculations
    {
        public readonly ILogger log;
        public readonly ISurveyRepository repo;

        public SurveyCalculations(ILogger<SurveyCalculations> log, ISurveyRepository repo)
        {
            this.log = log;
            this.repo = repo;
        }

        public object GetSurveysCountFromDate(DateTime Date)
        {
            return repo.GetClientsQuery()
                .AsEnumerable()
                .Where(x => x.Created > Date)
                .GroupBy(x => x.Created)
                .Select(x => new
                {
                    date = x.Key,
                    count = x.Count()
                });
        }
    }
}
