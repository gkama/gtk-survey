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
                .Workspaces
                .AddRangeAsync(GetFakeWorkspaces());

            await context
                .Surveys
                .AddRangeAsync(GetFakeSurveys());

            await context
                .Questions
                .AddRangeAsync(GetFakeQuestions());

            await context
                .SurveyQuestions
                .AddRangeAsync(GetFakeSurveyQuestions());

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

        public IEnumerable<Client> GetFakeClients()
        {
            return new List<Client>()
            {
                new Client()
                {
                    Id = 501,
                    Name = "Five Hundred and One",
                    Slug = "five-hundred-and-one",
                    Created = DateTime.Now.AddDays(-7),
                    LastUpdated = DateTime.Now.AddDays(-7),
                    BillingId = 9999,
                    PublicKey = new Guid("0aa7eacd-75d0-4f80-a875-98d3e096b0f9")
                }
            };
        }

        public IEnumerable<Workspace> GetFakeWorkspaces()
        {
            return new List<Workspace>()
            {
                new Workspace()
                {
                    Id = 999,
                    Name = "GTK Fake Workspace",
                    Slug = "gtk-fake",
                    Created = DateTime.Now.AddDays(-6),
                    LastUpdated = DateTime.Now.AddDays(-6),
                    ClientId = 501,
                    PublicKey = new Guid("2bc672e5-17d1-4dca-a59c-88413e45bfd4")
                },
                new Workspace()
                {
                    Id = 888,
                    Name = "GTK Migration US",
                    Slug = "gtk-migration-us",
                    Created = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    ClientId = null,
                    PublicKey = new Guid("6035ac1d-4440-4bdb-976b-98b75666aa8d")
                }
            };
        }

        public IEnumerable<Survey> GetFakeSurveys()
        {
            return new List<Survey>()
            {
                new Survey
                {
                    Id = 1001,
                    Name = "Test survey",
                    CreationDate = DateTime.Now.AddDays(-3),
                    CreatedBy = "Georgi",
                    LastUpdated = DateTime.Now.AddDays(-2),
                    LastUpdatedBy = "Jim",
                    WorkspaceId = 999,
                    PublicKey = Guid.NewGuid()
                },
                new Survey
                {
                    Id = 2001,
                    Name = "Survey Migration Test",
                    CreationDate = DateTime.Now.AddDays(-7),
                    CreatedBy = "Georgi",
                    LastUpdated = DateTime.Now.AddDays(-7),
                    LastUpdatedBy = "Jessie",
                    WorkspaceId = 888,
                    PublicKey = Guid.NewGuid()
                },
                new Survey
                {
                    Id = 3001,
                    Name = "Survey Test Empty",
                    CreationDate = DateTime.Now,
                    CreatedBy = "Georgi",
                    LastUpdated = DateTime.Now,
                    LastUpdatedBy = "Georgi",
                    WorkspaceId = null,
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
                    CreationDate = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    TypeId = 1,
                },
                new Question
                {
                    Id = 2,
                    Name = "Q2",
                    Text = "How are you today?",
                    PublicKey = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    TypeId = 2
                },
                new Question
                {
                    Id = 3,
                    Name = "Q3",
                    Text = "Are you cool?",
                    PublicKey = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    TypeId = 1
                },
                new Question
                {
                    Id = 4,
                    Name = "Q4",
                    Text = "Are you not cool?",
                    PublicKey = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    TypeId = 1
                }
            };
        }

        public IEnumerable<SurveyQuestion> GetFakeSurveyQuestions()
        {
            return new List<SurveyQuestion>()
            {
                new SurveyQuestion()
                {
                    Id = 1,
                    SurveyId = 1001,
                    QuestionId = 1,
                    PublicKey = Guid.NewGuid()
                },
                new SurveyQuestion()
                {
                    Id = 2,
                    SurveyId = 1001,
                    QuestionId = 2,
                    PublicKey = Guid.NewGuid()
                },
                new SurveyQuestion()
                {
                    Id = 3,
                    SurveyId = 2001,
                    QuestionId = 3,
                    PublicKey = Guid.NewGuid()
                },
                new SurveyQuestion
                {
                    Id = 4,
                    SurveyId = 2001,
                    QuestionId = 4,
                    PublicKey = Guid.NewGuid()
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
                    SurveyQuestionId = 1,
                    QuestionTypeAnswerId = 21
                },
                new Response
                {
                    Id = 2,
                    Count = 200,
                    PublicKey = Guid.NewGuid(),
                    SurveyQuestionId = 1,
                    QuestionTypeAnswerId = 22
                },
                new Response
                {
                    Id = 3,
                    Count = 23,
                    PublicKey = Guid.NewGuid(),
                    SurveyQuestionId = 2,
                    QuestionTypeAnswerId = 23
                },
                new Response
                {
                    Id = 4,
                    Count = 578,
                    PublicKey = Guid.NewGuid(),
                    SurveyQuestionId = 3,
                    QuestionTypeAnswerId = 23
                },
                new Response
                {
                    Id = 5,
                    Count = 214,
                    PublicKey = Guid.NewGuid(),
                    SurveyQuestionId = 4,
                    QuestionTypeAnswerId = 24
                }
            };
        }
    }
}
