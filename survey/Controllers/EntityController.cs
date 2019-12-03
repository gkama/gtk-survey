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
    [ApiController]
    [Route("entity")]
    public class EntityController : ControllerBase
    {
        public readonly ISurveyRepository repo;

        public EntityController(ISurveyRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [Route("question/{publickeyid}")]
        public async Task<IActionResult> GetEntityAsync([FromRoute]object PublicKeyId)
        {
            return Ok(await repo.FindEntityAsync<Question>(PublicKeyId));
        }
    }
}