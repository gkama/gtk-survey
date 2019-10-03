using System;

using Microsoft.Extensions.Logging;

using survey.data;

namespace survey.services
{
    public class SurveyGenerator : ISurveyGenerator
    {
        public readonly ILogger log;

        public ISurveyRepository repo;

        public SurveyGenerator(ILogger<SurveyGenerator> log, ISurveyRepository repo)
        {
            this.log = log;

            this.repo = repo;
        }

        public void CreateSurvey()
        {

        }
    }
}
