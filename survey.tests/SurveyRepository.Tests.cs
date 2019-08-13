using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Xunit;

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
    }
}
