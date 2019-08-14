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
    [Route("entity")]
    [ApiController]
    public class EntityController : ControllerBase
    {
        public readonly ISurveyRepository repo;

        public EntityController(ISurveyRepository repo)
        {
            this.repo = repo;
        }

        [Route("question/{publickeyid}")]
        [HttpGet]
        public async Task<IActionResult> GetEntityAsync([FromRoute]object PublicKeyId)
        {
            return Ok(await repo.FindEntityAsync<Question>(PublicKeyId));
        }
    }
}