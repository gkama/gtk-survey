using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
