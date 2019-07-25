﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using survey.data;
using survey.services;

namespace survey.Controllers
{
    [Route("survey")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        public readonly ISurveyRepository repo;

        public SurveyController(ISurveyRepository repo)
        {
            this.repo = repo;
        }

        [Route("all")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(repo.GetSurveys());
        }

        [Route("id/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetSurvey([FromRoute]int id)
        {
            return Ok(await repo.GetSurveyAsync(id));
        }

        [Route("publickey/{publickey}")]
        [HttpGet]
        public async Task<IActionResult> GetSurvey([FromRoute]Guid publickey)
        {
            return Ok(await repo.GetSurveyAsync(publickey));
        }

        [Route("questions")]
        [HttpGet]
        public IActionResult GetSurveyQuestions()
        {
            return Ok(repo.GetSurveyQuestions());
        }

        [Route("responses")]
        [HttpGet]
        public IActionResult GetResponses()
        {
            return Ok(repo.GetResponses());
        }

        [Route("{id}/responses")]
        [HttpGet]
        public async Task<IActionResult> GetResponsesCustomBySurveyIdAsync([FromRoute]int id)
        {
            return Ok(await repo.GetResponsesCustomBySurveyIdAsync(id));
        }

        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> CreateSurveyAsync([FromBody]string survey_name, IEnumerable<QuestionRequest> questions)
        {
            return Ok(await repo.CreateSurveyAsync(survey_name, questions));
        }

        [Route("{id}/response/update/{questionid}/{answer}")]
        [HttpGet]
        public async Task<IActionResult> UpdateResponse([FromRoute]int id, int questionid, string answer)
        {
            return Ok(await repo.UpdateResponseAsync(id, questionid, answer));
        }
    }
}
