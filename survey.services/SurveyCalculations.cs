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

        public void GetAverageSurveysAsync(DateTime Date)
        {
            var surveys = repo.GetClientsQuery()
                .AsEnumerable()
                .Where(x => x.LastUpdated <= Date);

            var counts = surveys.Count();
        }
    }
}
