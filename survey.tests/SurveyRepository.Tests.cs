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
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task ContextQuestionExist(int id)
        {
            Assert.True(await context.Questions
                .FirstOrDefaultAsync(x => x.Id == id) != null);
        }

        [Fact]
        public void EntityChanged()
        {
            Assert.False(repo.EntityChanged(context.Questions.FirstOrDefault(x => x.Id == 1)));
        }
    }
}
