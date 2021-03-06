﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using survey.data;
using survey.services;

namespace survey.Controllers
{
    [ApiController]
    [Route("survey/calculations")]
    public class SurveyCalculationsController : ControllerBase
    {
        public readonly ISurveyCalculations surveyCalc;

        public SurveyCalculationsController(ISurveyCalculations surveyCalc)
        {
            this.surveyCalc = surveyCalc;
        }

        [HttpGet]
        [Route("clients/by/date/{date}")]
        public IActionResult GetClientsByDate([FromRoute]DateTime date)
        {
            return Ok(surveyCalc.GetGenericCountFromDate<data.Client>(date));
        }

        [HttpGet]
        [Route("workspaces/by/date/{date}")]
        public IActionResult GetWorkspacesByDate([FromRoute]DateTime date)
        {
            return Ok(surveyCalc.GetGenericCountFromDate<data.Workspace>(date));
        }

        [HttpGet]
        [Route("questions/by/date/{date}")]
        public IActionResult GetQuestionsByDate([FromRoute]DateTime date)
        {
            return Ok(surveyCalc.GetGenericCountFromDate<Question>(date));
        }

        [HttpGet]
        [Route("surveys/by/date/{date}")]
        public IActionResult GetSurveysByDate([FromRoute]DateTime date)
        {
            return Ok(surveyCalc.GetGenericCountFromDate<Survey>(date));
        }
    }
}