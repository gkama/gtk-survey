using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using survey.data;
using survey.services;

namespace survey.Controllers
{
    [Route("survey/calculations")]
    [ApiController]
    public class SurveyCalculationsController : ControllerBase
    {
        public readonly ISurveyCalculations surveyCalc;

        public SurveyCalculationsController(ISurveyCalculations surveyCalc)
        {
            this.surveyCalc = surveyCalc;
        }

        [HttpGet]
        [Route("clients/by/date")]
        public IActionResult Get()
        {
            return Ok(surveyCalc.GetClientsCountFromDate(DateTime.Now.AddDays(-10)));
        }
    }
}