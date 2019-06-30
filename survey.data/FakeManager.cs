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
                .Surveys
                .AddRangeAsync(GetFakeSurveys());

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

        public IEnumerable<Survey> GetFakeSurveys()
        {
            return new List<Survey>()
            {
                new Survey
                {
                    Id = 1001,
                    Name = "Test survey",
                    PublicKey = Guid.NewGuid()
                }
            };
        }

        public IEnumerable<Question> GetFakeQuestions()
        {
            return new List<Question>()
            {
                new Question
                {
                    Id = 1,
                    Name = "Q1",
                    Text = "Are you awesome?",
                    PublicKey = Guid.NewGuid(),
                    TypeId = 1,
                },
                new Question
                {
                    Id = 2,
                    Name = "Q2",
                    Text = "How are you today?",
                    PublicKey = Guid.NewGuid(),
                    TypeId = 2
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
                },
                new QuestionTypeAnswer
                {
                    Id = 23,
                    TypeId = 2,
                    Answer = "Good",
                    PublicKey = Guid.NewGuid()
                },
                new QuestionTypeAnswer
                {
                    Id = 24,
                    TypeId = 2,
                    Answer = "Awesome! And you?",
                    PublicKey = Guid.NewGuid()
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
                    Count = 102,
                    PublicKey = Guid.NewGuid(),
                    QuestionId = 1,
                    QuestionTypeAnswerId = 21
                },
                new Response
                {
                    Id = 2,
                    Count = 23,
                    PublicKey = Guid.NewGuid(),
                    QuestionId = 1,
                    QuestionTypeAnswerId = 22
                },
                new Response
                {
                    Id = 3,
                    Count = 578,
                    PublicKey = Guid.NewGuid(),
                    QuestionId = 2,
                    QuestionTypeAnswerId = 23
                },
                new Response
                {
                    Id = 4,
                    Count = 214,
                    PublicKey = Guid.NewGuid(),
                    QuestionId = 2,
                    QuestionTypeAnswerId = 24
                }
            };
        }
    }
}
