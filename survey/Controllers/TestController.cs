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
    [Route("test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public readonly ISurveyRepository repo;

        public TestController(ISurveyRepository repo)
        {
            this.repo = repo;
        }
    }
}