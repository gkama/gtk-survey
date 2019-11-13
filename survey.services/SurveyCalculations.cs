using System;
using System.Collections.Generic;
using System.Text;

namespace survey.services
{
    public class SurveyCalculations : ISurveyCalculations
    {
        public readonly ISurveyRepository SurveyRepository;

        public SurveyCalculations(ISurveyRepository SurveyRepository)
        {
            this.SurveyRepository = SurveyRepository;
        }
    }
}
