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
    [Route("test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public readonly ISurveyRepository repo;

        public TestController(ISurveyRepository repo)
        {
            this.repo = repo;
        }

        [Route("update/{iterations}")]
        [HttpGet]
        public void UpdateResponse([FromRoute]int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                repo.UpdateResponse(2001, 3, "Good");
            }
        }
    }
}