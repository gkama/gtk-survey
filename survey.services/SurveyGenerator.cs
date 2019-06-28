using System;

using Microsoft.Extensions.Logging;

using survey.data;

namespace survey.services
{
    public class SurveyGenerator : ISurveyGenerator
    {
        public readonly ILogger log;

        public SurveyGenerator(ILogger<SurveyGenerator> log)
        {
            this.log = log;
        }
    }
}
