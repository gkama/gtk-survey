using System;
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

        [Route("{id}/responses/count")]
        [HttpGet]
        public async Task<IActionResult> GetResponsesCountAsync([FromRoute]int id)
        {
            return Ok(await repo.GetResponsesCountAsync(id));
        }

        [Route("{id}/responses/stats")]
        [HttpGet]
        public async Task<IActionResult> GetResponsesStatsAsync([FromRoute]int id)
        {
            return Ok(await repo.GetResponsesStatsAsync(id));
        }

        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> CreateSurveyAsync([FromBody]SurveyQuestionRequest request)
        {
            return Ok(await repo.CreateSurveyAsync(request.SurveyName, request.QuestionRequests));
        }

        [Route("{id}/response/update/{questionid}/{answer}")]
        [HttpGet]
        public async Task<IActionResult> UpdateResponse([FromRoute]int id, int questionid, string answer)
        {
            return Ok(await repo.UpdateResponseAsync(id, questionid, answer));
        }
    }
}
