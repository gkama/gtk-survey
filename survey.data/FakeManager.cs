using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace survey.data
{
    public class FakeManager
    {
        private readonly SurveyContext context;

        public FakeManager(SurveyContext context)
        {
            this.context = context;
        }

        public async Task UseFakeContext()
        {
            await context
                .Questions
                .AddRangeAsync(GetFakeQuestions());

            await context
                .QuestionTypes
                .AddRangeAsync(GetFakeQuestionTypes());

            await context
                .QuestionTypeAnswers
                .AddRangeAsync(GetFakeQuestionTypeAnswers());

            await context
                .Responses
                .AddRangeAsync(GetFakeResponses());

            await context.SaveChangesAsync();
        }

        public IEnumerable<Question> GetFakeQuestions()
        {
            return new List<Question>()
            {
                new Question
                {
                    Id = 1,
                    PublicKey = Guid.NewGuid(),
                    Name = "Q1",
                    TypeId = 1,
                    Text = "Are you awesome?"
                },
                new Question
                {
                    Id = 2,
                    PublicKey = Guid.NewGuid(),
                    Name = "Q2",
                    TypeId = 2,
                    Text = "How are you today?"
                }
            };
        }

        public IEnumerable<Response> GetFakeResponses()
        {
            return new List<Response>()
            {
                new Response
                {
                    Id = 1,
                    QuestionId = 1,
                    PublicKey = Guid.NewGuid(),
                    Text = "Yes"
                },
                new Response
                {
                    Id = 2,
                    QuestionId = 1,
                    PublicKey = Guid.NewGuid(),
                    Text = "No"
                },
                new Response
                {
                    Id = 3,
                    QuestionId = 2,
                    PublicKey = Guid.NewGuid(),
                    Text = "Good"
                },
                new Response
                {
                    Id = 4,
                    QuestionId = 2,
                    PublicKey = Guid.NewGuid(),
                    Text = "Awesome! Thanks for asking"
                }
            };
        }

        public IEnumerable<QuestionType> GetFakeQuestionTypes()
        {
            return new List<QuestionType>()
            {
                new QuestionType
                {
                    Id = 1,
                    PublicKey = Guid.NewGuid(),
                    Name = "Yes or No"
                },
                new QuestionType
                {
                    Id = 2,
                    PublicKey = Guid.NewGuid(),
                    Name = "Open ended"
                }
            };
        }

        public IEnumerable<QuestionTypeAnswer> GetFakeQuestionTypeAnswers()
        {
            return new List<QuestionTypeAnswer>()
            {
                new QuestionTypeAnswer
                {
                    Id = 21,
                    TypeId = 1,
                    Answer = "Yes",
                    PublicKey = Guid.NewGuid()
                },
                new QuestionTypeAnswer
                {
                    Id = 22,
                    TypeId = 1,
                    Answer = "No",
                    PublicKey = Guid.NewGuid()
                }
            };
        }
    }
}
