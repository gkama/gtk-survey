using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetSurvey([FromRoute]int id)
        {
            return Ok(repo.GetSurvey(id));
        }

        [Route("publickey/{publickey}")]
        [HttpGet]
        public IActionResult GetSurvey([FromRoute]Guid publickey)
        {
            return Ok(repo.GetSurvey(publickey));
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

        [Route("response/update/{surveyid}/{questionid}/{answer}")]
        [HttpGet]
        public async Task<IActionResult> UpdateResponse([FromRoute]int surveyid, int questionid, string answer)
        {
            return Ok(await repo.UpdateResponse(surveyid, questionid, answer));
        }
    }
}
