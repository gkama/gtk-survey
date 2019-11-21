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

        [HttpGet]
        [Route("all")]
        public IActionResult Get()
        {
            return Ok(repo.GetSurveys());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSurveyAsync([FromRoute]int id)
        {
            return Ok(await repo.GetSurveyAsync(id));
        }

        [HttpGet]
        [Route("categories")]
        public IActionResult GetSurveyCategories()
        {
            return Ok(repo.GetSurveyCategories());
        }

        [HttpGet]
        [Route("category/{id}")]
        public async Task<IActionResult> GetSurveyCategoryAsync([FromRoute]int id)
        {
            return Ok(await repo.GetSurveyCategoryAsync(id));
        }

        [HttpGet]
        [Route("category/{name}")]
        public async Task<IActionResult> GetSurveyCategoryAsync([FromRoute]string name)
        {
            return Ok(await repo.GetSurveyCategoryAsync(name));
        }

        [HttpGet]
        [Route("category/add/{name}")]
        public async Task<IActionResult> AddSurveyCategoryAsync([FromRoute]string name)
        {
            await repo.AddSurveyCategoryAsync(name);

            return Ok("success");
        }

        [HttpGet]
        [Route("{id}/questions")]
        public async Task<IActionResult> GetSurveyQuestionsAsync([FromRoute]int id)
        {
            return Ok(await repo.GetSurveyQuestionsAsync(id));
        }

        [HttpGet]
        [Route("publickey/{publickey}")]
        public async Task<IActionResult> GetSurvey([FromRoute]Guid publickey)
        {
            return Ok(await repo.GetSurveyAsync(publickey));
        }

        [HttpGet]
        [Route("responses")]
        public IActionResult GetResponses()
        {
            return Ok(repo.GetResponses());
        }

        [HttpGet]
        [Route("{id}/responses")]
        public async Task<IActionResult> GetResponsesCustomBySurveyIdAsync([FromRoute]int id)
        {
            return Ok(await repo.GetResponsesStatsAsync(id));
        }

        [HttpGet]
        [Route("{id}/responses/count")]
        public async Task<IActionResult> GetResponsesCountAsync([FromRoute]int id)
        {
            return Ok(await repo.GetResponsesCountAsync(id));
        }

        [HttpGet]
        [Route("{id}/responses/stats")]
        public async Task<IActionResult> GetResponsesStatsAsync([FromRoute]int id)
        {
            return Ok(await repo.GetResponsesStatsAsync(id));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateSurveyAsync([FromBody]SurveyQuestionRequest request)
        {
            return Ok(await repo.CreateSurveyAsync(request.SurveyName, request.QuestionRequests));
        }

        [HttpGet]
        [Route("{id}/response/update/{questionid}/{answer}")]
        public async Task<IActionResult> UpdateResponseAsync([FromRoute]int id, int questionid, string answer)
        {
            return Ok(await repo.UpdateResponseAsync(id, questionid, answer));
        }

        [HttpGet]
        [Route("today")]
        public async Task<IActionResult> GetSurveysTodayAsync()
        {
            return Ok(await repo.GetSurveysToday());
        }

        [HttpGet]
        [Route("today/count")]
        public async Task<IActionResult> GetSurveysTodayCountAsync()
        {
            return Ok(await repo.GetSurveysTodayCount());
        }
    }
}
