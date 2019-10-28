using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Xunit;
using Newtonsoft.Json;

using survey.data;
using survey.services;

namespace survey.tests
{
    public class SurveyRepositoryTests
    {
        public readonly SurveyContext context;

        public readonly ISurveyRepository repo;

        public SurveyRepositoryTests()
        {
            context = new SurveyContext(new DbContextOptionsBuilder<SurveyContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);

           new FakeManager(context)
                .UseFakeContext()
                .Wait();

            this.repo = new SurveyRepository(null, context);
        }

        /*
         * Survey
         */
        [Theory]
        [InlineData(1001)]
        [InlineData(2001)]
        public async Task Survey_Exists_Id(int id)
        {
            var survey = await repo.GetSurveyAsync(id);

            //Asserts
            Assert.True(survey != null);
            Assert.True(!string.IsNullOrWhiteSpace(survey.Name));
            Assert.True(!string.IsNullOrWhiteSpace(survey.PublicKey.ToString()));
        }
        [Theory]
        [InlineData("Test survey")]
        [InlineData("Survey Test")]
        public async Task Survey_Exists_Name(string name)
        {
            var survey = await repo.GetSurveyAsync(name);

            //Asserts
            Assert.True(survey != null);
            Assert.True(!string.IsNullOrWhiteSpace(survey.Name));
            Assert.True(!string.IsNullOrWhiteSpace(survey.PublicKey.ToString()));
        }



        /*
         * Survey Categories
         */
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(9)]
        public async Task SurveyCategoriesExist(int id)
        {
            var surveyCategory = await repo.GetSurveyCategoryAsync(id);

            //Asserts
            Assert.True(surveyCategory != null);
            Assert.True(!string.IsNullOrWhiteSpace(surveyCategory.Name));
        }



        /*
         * Question
         */
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task Question_Context_Exists(int id)
        {
            //Asserts
            Assert.True(await context.Questions
                .FirstOrDefaultAsync(x => x.Id == id) != null);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task Question_Exists_Id(int id)
        {
            //Asserts
            Assert.True(await repo.GetQuestionAsync(id) != null);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public async Task Question_Not_Exists(int id)
        {
            //Asserts
            Assert.True(await repo.GetQuestionAsync(id) == null);
        }



        /*
         * Full tests
         */
        [Fact]
        public void FullSurvey()
        {
            /*
             * 1) Create survey
             * 2) Update responses
             * 3) Check responses
             */
            var questionRequests = new List<QuestionRequest>()
            {
                new QuestionRequest()
                {
                    QuestionName = "GTK Q1",
                    QuestionText = "On a scale of 1 to 10, how much do you like cheetos?",
                    QuestionTypeName = "Cheetos 1 to 10",
                    QuestionTypeAnswers = new List<string>()
                    {
                        "1", "2", "3", "4", "5", "6", "7", "8", "9", "10"
                    }
                },
                new QuestionRequest()
                {
                    QuestionName = "GTK Q2",
                    QuestionText = "Do you like spicy food",
                    QuestionTypeName = "Yes or No",
                    QuestionTypeAnswers = new List<string>()
                    {
                        "Yes", "No"
                    }
                },
                new QuestionRequest()
                {
                    QuestionName = "GTK Q3",
                    QuestionText = "Where are you from?",
                    QuestionTypeName = "Open ended"
                }
            };
            var reqs = JsonConvert.SerializeObject(questionRequests);

            //Asserts
            Assert.True(reqs != null);
        }



        /*
         * General
         */
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task EntityChanged_Question_False(int id)
        {
            //Asserts
            Assert.False(repo.EntityChanged(await context.Questions
                .FirstOrDefaultAsync(x => x.Id == id)));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task EntityChanged_Question_True(int id)
        {
            var question = await context.Questions
                .FirstOrDefaultAsync(x => x.Id == id);

            question.Name = $"Updating name to {id}";

            //Asserts
            Assert.True(repo.EntityChanged(question));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task FindEntityAsync_Question(int id)
        {
            var entity = await repo.FindEntityAsync<Question>(id);

            //Asserts
            Assert.True(entity != null);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task FindEntityAsync_Question_PublicKey(int id)
        {
            var question =  await repo.GetQuestionAsync(id);
            var entity = await repo.FindEntityAsync<Question>(question.PublicKey);

            //Asserts
            Assert.True(entity != null);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task FindEntityAsync_Question_PublicKeyId(object PublicKeyId)
        {
            var entity = await repo.FindEntityAsync<Question>(PublicKeyId);

            //Asserts
            Assert.True(entity != null);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task FindEntityPublicKeyAsync_Question(int id)
        {
            var publicKey = await repo.FindEntityPublicKeyAsync<Question>(id);

            //Asserts
            Assert.True(publicKey != Guid.Empty);
        }
    }
}
