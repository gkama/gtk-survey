using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using survey.data;

namespace survey.services
{
    public class SurveyRepository : ISurveyRepository
    {
        public readonly ILogger log;

        public readonly SurveyContext context;

        public SurveyRepository(ILogger<SurveyRepository> log, SurveyContext context)
        {
            this.log = log;

            this.context = context;
        }



        /*
         * Client
         */
        public IQueryable<Client> GetClientsQuery()
        {
            return context.Clients
                .Include(x => x.Workspaces)
                    .ThenInclude(x => x.Surveys)
                .AsQueryable();
        }
        public IQueryable<Client> GetClientsQuerySimplified()
        {
            return context.Clients
                .Include(x => x.Workspaces)
                .AsQueryable();
        }

        public IEnumerable<Client> GetClients()
        {
            return GetClientsQuerySimplified()
                .AsEnumerable();
        }
        public Client GetClient(string Name, string Slug)
        {
            return context.Clients
                .FirstOrDefault(x => x.Name == Name
                    && x.Slug.Equals(Slug, StringComparison.OrdinalIgnoreCase));
        }
        public Client GetClientByName(string Name)
        {
            return context.Clients
                .FirstOrDefault(x => x.Name == Name);
        }
        public Client GetClientBySlug(string Slug)
        {
            return context.Clients
                .FirstOrDefault(x => x.Slug.Equals(Slug, StringComparison.OrdinalIgnoreCase));
        }
        public async Task<Client> GetClientAsync(int Id)
        {
            return await GetClientsQuerySimplified()
                .FirstOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<Client> GetClientAsync(Guid PublicKey)
        {
            return await GetClientsQuerySimplified()
                .FirstOrDefaultAsync(x => x.PublicKey == PublicKey);
        }
        public async Task<Client> GetClientAsync(string Name, string Slug)
        {
            return await context.Clients
                .FirstOrDefaultAsync(x => x.Slug.Equals(Slug, StringComparison.OrdinalIgnoreCase)
                    && x.Name == Name);
        }
        public async Task<Client> GetClientByNameAsync(string Name)
        {
            return await context.Clients
                .FirstOrDefaultAsync(x => x.Name == Name);
        }
        public async Task<Client> GetClientBySlugAsync(string Slug)
        {
            return await context.Clients
                .FirstOrDefaultAsync(x => x.Slug.Equals(Slug, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Client> AddClientAsync(string Name, string Slug, int? BillingId)
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Slug))
                throw new SurveyException(HttpStatusCode.BadRequest, $"cannot add a Client with empty or null Name='{Name}' or Slug='{Slug}'");

            var client = await GetClientAsync(Name, Slug);

            if (client == null)
            {
                client = new Client()
                {
                    Name = Name,
                    Slug = Slug,
                    Created = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    BillingId = BillingId ?? null,
                    PublicKey = Guid.NewGuid()
                };

                await context.Clients
                    .AddAsync(client);

                await context.SaveChangesAsync();
            }

            return client;
        }
        public void AddClient(string Name, string Slug, int? BillingId)
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Slug))
                throw new SurveyException(HttpStatusCode.BadRequest, $"cannot add a Client with empty or null Name='{Name}' or Slug='{Slug}'");

            var client = GetClient(Name, Slug);

            if (client == null)
            {
                client = new Client()
                {
                    Name = Name,
                    Slug = Slug,
                    Created = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    BillingId = BillingId ?? null,
                    PublicKey = Guid.NewGuid()
                };

                context.Clients
                    .Add(client);

                context.SaveChanges();
            }
        }

        public async Task DeleteClientAsync(string Name, string Slug)
        {
            try
            {
                context.Clients
                    .Remove(await GetClientAsync(Name, Slug));

                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new SurveyException($"error while deleting client with name={Name} slug={Slug}. error={e.Message}");
            }

            log.LogInformation($"request to DELETE Client with Name='{Name}' and Slug='{Slug}' COMPLETED");
        }



        /*
         * Workspace
         */
        public IQueryable<Workspace> GetWorkspacesQuery()
        {
            return context.Workspaces
                .AsQueryable();
        }

        public IEnumerable<Workspace> GetWorkspaces()
        {
            return GetWorkspacesQuery()
                .AsEnumerable();
        }
        public async Task<Workspace> GetWorkspaceAsync(int Id)
        {
            return await GetWorkspacesQuery()
                .FirstOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<Workspace> GetWorkspaceAsync(Guid PublicKey)
        {
            return await GetWorkspacesQuery()
                .FirstOrDefaultAsync(x => x.PublicKey == PublicKey);
        }
        public async Task<Workspace> GetWorkspaceAsync(string Name, string Slug)
        {
            return await context.Workspaces
                .FirstOrDefaultAsync(x => x.Name == Name
                    && x.Slug.Equals(Slug, StringComparison.OrdinalIgnoreCase));
        }
        public async Task<Workspace> GetWorkspaceByNameAsync(string Name)
        {
            return await context.Workspaces
                .FirstOrDefaultAsync(x => x.Name == Name);
        }
        public async Task<Workspace> GetWorkspaceBySlugAsync(string Slug)
        {
            return await context.Workspaces
                .FirstOrDefaultAsync(x => x.Slug.Equals(Slug, StringComparison.OrdinalIgnoreCase));
        }
        public Workspace GetWorkspace(string Name, string Slug)
        {
            return context.Workspaces
                .FirstOrDefault(x => x.Name == Name
                    && x.Slug.Equals(Slug, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Workspace> AddWorkspaceAsync(string Name, string Slug, int? ClientId)
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Slug))
                throw new SurveyException(HttpStatusCode.BadRequest, $"cannot add a Workspace with empty or null Name='{Name}' or Slug='{Slug}'");

            var workspace = await GetWorkspaceAsync(Name, Slug);

            if (workspace == null)
            {
                workspace = new Workspace()
                {
                    Name = Name,
                    Slug = Slug,
                    Created = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    ClientId = ClientId ?? null,
                    PublicKey = Guid.NewGuid()
                };

                await context.Workspaces
                    .AddAsync(workspace);

                await context.SaveChangesAsync();
            }

            return workspace;
        }
        public void AddWorkspace(string Name, string Slug, int? ClientId)
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Slug))
                throw new SurveyException(HttpStatusCode.BadRequest, $"cannot add a Workspace with empty or null Name='{Name}' or Slug='{Slug}'");

            var workspace = GetWorkspace(Name, Slug);

            if (workspace == null)
            {
                workspace = new Workspace()
                {
                    Name = Name,
                    Slug = Slug,
                    Created = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    ClientId = ClientId ?? null,
                    PublicKey = Guid.NewGuid()
                };

                context.Workspaces
                    .Add(workspace);

                context.SaveChanges();
            }
        }

        public async Task DeleteWorkspaceAsync(string Name, string Slug)
        {
            context.Workspaces
                .Remove(await GetWorkspaceAsync(Name, Slug));

            await context.SaveChangesAsync();

            log.LogInformation($"request to DELETE Workspace with Name='{Name}' and Slug='{Slug}' COMPLETED");
        }

        public async Task<object> GetWorkspaceSimpleStatsAsync(int Id)
        {
            var workspace = await GetWorkspaceAsync(Id);

            return new
            {
                id = workspace.Id,
                name = workspace.Name,
                public_key = workspace.PublicKey,
                surveys_count = workspace.Surveys.Count()
            };
        }



        /*
         * Survey
         */
        public IQueryable<Survey> GetSurveysQuery()
        {
            return context.Surveys
                .Include(x => x.Category)
                .Include(x => x.SurveyQuestions)
                    .ThenInclude(x => x.Question)
                        .ThenInclude(x => x.Type)
                            .ThenInclude(x => x.Answers)
                .AsQueryable();
        }
        public IQueryable<Survey> GetSurveysWithWorkspace()
        {
            return context.Surveys
                .Include(x => x.Workspace)
                .AsQueryable();
        }
        public IQueryable<Survey> GetSurveysSimplifiedQuery()
        {
            return context.Surveys
                .AsQueryable();
        }

        public IEnumerable<Survey> GetSurveys()
        {
            return GetSurveysQuery()
                .AsEnumerable();
        }
        public async Task<Survey> GetSurveyAsync(int Id)
        {
            return await GetSurveysQuery()
                .FirstOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<Survey> GetSurveyAsync(string Name)
        {
            return await GetSurveysQuery()
                .FirstOrDefaultAsync(x => x.Name == Name);
        }
        public async Task<Survey> GetSurveyAsync(Guid PublicKey)
        {
            return await GetSurveysQuery()
                .FirstOrDefaultAsync(x => x.PublicKey == PublicKey);
        }
        public async Task<IEnumerable<Survey>> GetSurveysToday()
        {
            return await GetSurveysQuery()
                .Where(x => x.Created.Day == DateTime.Now.Day)
                .ToListAsync();
        }
        public async Task<int> GetSurveysTodayCount()
        {
            return (await GetSurveysToday())
                .Count();
        }

        public async Task<Survey> AddSurveyAsync(string Name)
        {
            var survey = new Survey()
            {
                Name = Name,
                Created = DateTime.Now,
                CreatedBy = string.Empty,
                LastUpdated = DateTime.Now,
                LastUpdatedBy = string.Empty,
                PublicKey = Guid.NewGuid()
            };

            await context.Surveys
                .AddAsync(survey);

            await context.SaveChangesAsync();

            return survey;
        }

        public async Task<Survey> CreateSurveyAsync(string SurveyName, IEnumerable<Question> Questions)
        {
            try
            {
                var survey = await AddSurveyAsync(SurveyName);
                var questionIds = Questions.Select(x => x.Id);

                var surveyQuestions = await AddSurveyQuestionAsync(survey.Id, questionIds);

                return survey;
            }
            catch (Exception)
            {
                throw new SurveyException(HttpStatusCode.InternalServerError,
                    $"error while creating survey with survey name={SurveyName} questions count={Questions?.Count()}");
            }
        }
        public async Task<Survey> CreateSurveyAsync(string SurveyName, IEnumerable<QuestionRequest> QuestionRequests)
        {
            var questions = new List<Question>();

            QuestionRequests.ToList().ForEach(async x =>
            {
                questions.Add(await AddQuestionAsync(x.QuestionName, x.QuestionText,
                    x.QuestionTypeName, x.QuestionTypeAnswers));
            });

            return await CreateSurveyAsync(SurveyName, questions);
        }



        /*
         * Survey Categories
         */
        public IQueryable<SurveyCategory> GetSurveyCategoriesQuery()
        {
            return context.SurveyCategories
                .AsQueryable();
        }

        public IEnumerable<SurveyCategory> GetSurveyCategories()
        {
            return GetSurveyCategoriesQuery()
                .AsEnumerable();
        }

        public async Task<SurveyCategory> GetSurveyCategoryAsync(int Id)
        {
            return await context.SurveyCategories
                .FirstOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<SurveyCategory> GetSurveyCategoryAsync(Guid PublicKey)
        {
            return await context.SurveyCategories
                .FirstOrDefaultAsync(x => x.PublicKey == PublicKey);
        }
        public async Task<SurveyCategory> GetSurveyCategoryAsync(string Name)
        {
            return await context.SurveyCategories
                .FirstOrDefaultAsync(x => x.Name == Name);
        }

        public async Task AddSurveyCategoryAsync(string Name)
        {
            var surveyCategory = await GetSurveyCategoryAsync(Name);

            if (surveyCategory == null)
            {
                surveyCategory = new SurveyCategory()
                {
                    Name = Name,
                    PublicKey = Guid.NewGuid()
                };

                await context.SurveyCategories
                    .AddAsync(surveyCategory);

                await context.SaveChangesAsync();
            }
        }



        /*
         * Questions
         */
        public IQueryable<Question> GetQuestionsQuery()
        {
            return context.Questions
                .Include(x => x.Type)
                .AsQueryable();
        }

        public IEnumerable<Question> GetQuestions()
        {
            return GetQuestionsQuery()
                .AsEnumerable();
        }
        public async Task<Question> GetQuestionAsync(int Id)
        {
            return await GetQuestionsQuery()
                .FirstOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<Question> GetQuestionAsync(Guid PublicKey)
        {
            return await GetQuestionsQuery()
                .FirstOrDefaultAsync(x => x.PublicKey == PublicKey);
        }
        public async Task<Question> GetQuestionAsync(string Name, string Text, QuestionType Type)
        {
            return await GetQuestionsQuery()
                .FirstOrDefaultAsync(x => x.Name == Name
                    && x.Text == Text
                    && x.TypeId == Type.Id);
        }

        public async Task<Question> AddQuestionAsync(string Name, string Text, QuestionType Type)
        {
            var question = await GetQuestionAsync(Name, Text, Type);

            if (question == null)
            {
                question = new Question()
                {
                    Name = Name,
                    Text = Text,
                    TypeId = Type.Id,
                    Created = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    PublicKey = Guid.NewGuid()
                };

                await context.Questions
                    .AddAsync(question);

                await context.SaveChangesAsync();
            }

            return question;
        }
        public async Task<Question> AddQuestionAsync(string Name, string Text, string QuestionTypeName, IEnumerable<string> Answers)
        {
            try
            {
                var qtype = await AddQuestionTypeAsync(QuestionTypeName);
                var qtypeAnswers = await AddQuestionTypeAnswerAsync(qtype.Id, Answers);

                var question = await GetQuestionAsync(Name, Text, qtype);

                if (question == null)
                {
                    question = new Question()
                    {
                        Name = Name,
                        Text = Text,
                        TypeId = qtype.Id,
                        Created = DateTime.Now,
                        LastUpdated = DateTime.Now,
                        PublicKey = Guid.NewGuid()
                    };

                    await context.Questions
                        .AddAsync(question);

                    await context.SaveChangesAsync();
                }

                return question;
            }
            catch (Exception e)
            {
                throw new SurveyException(HttpStatusCode.InternalServerError,
                    $"error while adding question with name={Name} text={Text} question type name={QuestionTypeName} and a list of answers. exception={e.ToString()}");
            }
        }



        /*
         * Question Type
         */
        public IQueryable<QuestionType> GetQuestionTypesQuery()
        {
            return context.QuestionTypes
                .AsQueryable();
        }
        public IQueryable<QuestionType> GetQuestionTypesQueryWithAnswers()
        {
            return context.QuestionTypes
                .Include(x => x.Answers)
                .AsQueryable();
        }
        public IQueryable<QuestionType> GetQuestionTypesQueryWithAnswersWithoutOpenEnded()
        {
            return context.QuestionTypes
                .Include(x => x.Answers)
                .Where(x => x.Id != 1)
                .AsQueryable();
        }

        public IEnumerable<QuestionType> GetQuestionTypes()
        {
            return GetQuestionTypesQuery()
                .AsEnumerable();
        }
        public async Task<QuestionType> GetQuestionTypeAsync(string Name)
        {
            return await GetQuestionTypesQueryWithAnswers()
                .FirstOrDefaultAsync(x => x.Name == Name);
        }
        public async Task<QuestionType> GetQuestionTypeAsync(QuestionTypeAnswer Answer)
        {
            return await GetQuestionTypesQueryWithAnswers()
                .FirstOrDefaultAsync(x => x.Answers.Contains(Answer));
        }
        public async Task<QuestionType> GetQuestionTypeAsync(string Name, IEnumerable<QuestionTypeAnswer> Answers)
        {
            return await GetQuestionTypesQueryWithAnswers()
                .FirstOrDefaultAsync(x => x.Name == Name
                    && x.Answers == Answers);
        }

        public async Task<QuestionType> AddQuestionTypeAsync(string Name)
        {
            var questionType = await GetQuestionTypeAsync(Name);

            if (questionType == null)
            {
                questionType = new QuestionType()
                {
                    Name = Name,
                    PublicKey = Guid.NewGuid()
                };

                await context.QuestionTypes
                    .AddAsync(questionType);

                await context.SaveChangesAsync();
            }

            return questionType;
        }
        public async Task<QuestionType> AddQuestionTypeAsync(string Name, ICollection<QuestionTypeAnswer> Answers)
        {
            var questionType = await GetQuestionTypeAsync(Name, Answers);

            if (questionType == null)
            {
                questionType = new QuestionType()
                {
                    Name = Name,
                    PublicKey = Guid.NewGuid()
                };

                await context.QuestionTypes
                    .AddAsync(questionType);

                await context.SaveChangesAsync();
            }

            return questionType;
        }



        /*
         * Question Type Answer
         */
        public IQueryable<QuestionTypeAnswer> GetQuestionTypeAnswersQuery()
        {
            return context.QuestionTypeAnswers
                .AsQueryable();
        }

        public IEnumerable<QuestionTypeAnswer> GetQuestionTypeAnswers()
        {
            return GetQuestionTypeAnswersQuery()
                .AsEnumerable();
        }
        public async Task<QuestionTypeAnswer> GetQuestionTypeAnswerAsync(string Answer)
        {
            return await GetQuestionTypeAnswersQuery()
                .FirstOrDefaultAsync(x => x.Answer == Answer);
        }
        public async Task<QuestionTypeAnswer> GetQuestionTypeAnswerAsync(int TypeId)
        {
            return await GetQuestionTypeAnswersQuery()
                .FirstOrDefaultAsync(x => x.TypeId == TypeId);
        }
        public IEnumerable<QuestionTypeAnswer> GetQuestionTypeAnswer(int TypeId)
        {
            return GetQuestionTypeAnswersQuery()
                .Where(x => x.TypeId == TypeId);
        }
        public async Task<QuestionTypeAnswer> GetQuestionTypeAnswerAsync(QuestionType Type)
        {
            return await GetQuestionTypeAnswersQuery()
                .FirstOrDefaultAsync(x => x.TypeId == Type.Id);
        }

        public async Task<QuestionTypeAnswer> AddQuestionTypeAnswerAsync(string Answer, int TypeId)
        {
            var qtypeAnswer = await GetQuestionTypeAnswerAsync(Answer);

            if (qtypeAnswer == null)
            {
                qtypeAnswer = new QuestionTypeAnswer()
                {
                    TypeId = TypeId,
                    Answer = Answer,
                    PublicKey = Guid.NewGuid()
                };

                await context.QuestionTypeAnswers
                    .AddAsync(qtypeAnswer);

                await context.SaveChangesAsync();
            }

            return qtypeAnswer;
        }
        public async Task<IEnumerable<QuestionTypeAnswer>> AddQuestionTypeAnswerAsync(int TypeId, IEnumerable<string> Answers)
        {
            var qtypeAnswers = new List<QuestionTypeAnswer>();

            foreach (var a in Answers)
                qtypeAnswers.Add(await AddQuestionTypeAnswerAsync(a, TypeId));

            return qtypeAnswers
                .AsEnumerable();
        }



        /*
         * Survey Question
         */
        public IQueryable<SurveyQuestion> GetSurveyQuestionsQuery()
        {
            return context.SurveyQuestions
                .Include(x => x.Survey)
                .Include(x => x.Question)
                    .ThenInclude(x => x.Type)
                .GroupBy(x => x.Survey)
                .SelectMany(x => x)
                .AsQueryable();
        }

        public IEnumerable<SurveyQuestion> GetSurveyQuestions()
        {
            return GetSurveyQuestionsQuery()
                .AsEnumerable();
        }
        public async Task<IEnumerable<SurveyQuestion>> GetSurveyQuestionsAsync(int SurveyId)
        {
            return await GetSurveyQuestionsQuery()
                .Where(x => x.SurveyId == SurveyId)
                .ToListAsync();
        }
        public IEnumerable<SurveyQuestion> GetSurveyQuestionsByQuestionId(int QuestionId)
        {
            return GetSurveyQuestionsQuery()
                .Where(x => x.QuestionId == QuestionId);
        }
        public async Task<SurveyQuestion> GetSurveyQuestion(int SurveyId, int QuestionId)
        {
            return await context.SurveyQuestions
                .FirstOrDefaultAsync(x => x.SurveyId == SurveyId
                    && x.QuestionId == QuestionId);
        }

        public async Task<SurveyQuestion> AddSurveyQuestionAsync(int SurveyId, int QuestionId)
        {
            var surveyQuestion = await GetSurveyQuestion(SurveyId, QuestionId);

            if (surveyQuestion == null)
            {
                surveyQuestion = new SurveyQuestion()
                {
                    SurveyId = SurveyId,
                    QuestionId = QuestionId,
                    PublicKey = Guid.NewGuid()
                };

                await context.SurveyQuestions
                    .AddAsync(surveyQuestion);

                await context.SaveChangesAsync();
            }

            return surveyQuestion;
        }
        public async Task<IEnumerable<SurveyQuestion>> AddSurveyQuestionAsync(int SurveyId, IEnumerable<int> QuestionIds)
        {
            var surveyQuestions = (await GetSurveyQuestionsAsync(SurveyId)).ToList();

            if (surveyQuestions == null || surveyQuestions?.Count() == 0)
            {
                QuestionIds.ToList().ForEach(qId =>
                {
                    surveyQuestions.Add(new SurveyQuestion()
                    {
                        SurveyId = SurveyId,
                        QuestionId = qId,
                        PublicKey = Guid.NewGuid()
                    });
                });

                await context.SurveyQuestions
                    .AddRangeAsync(surveyQuestions);

                await context.SaveChangesAsync();
            }

            return surveyQuestions;
        }



        /*
         * Response
         */
        public IQueryable<Response> GetResponsesQuery()
        {
            return context.Responses
                .Include(x => x.SurveyQuestion)
                    .ThenInclude(x => x.Survey)
                .Include(x => x.SurveyQuestion)
                    .ThenInclude(x => x.Question)
                .Include(x => x.QuestionTypeAnswer)
                .AsQueryable();
        }
        public IQueryable<Response> GetResponsesQuerySimplified()
        {
            return context.Responses
                .Include(x => x.SurveyQuestion)
                .Include(x => x.QuestionTypeAnswer)
                .AsQueryable();
        }
        public IQueryable<Response> GetResponsesSimplifiedQuery(int SurveyId)
        {
            return context.Responses
                .Where(x => x.SurveyQuestion.SurveyId == SurveyId)
                .Include(x => x.SurveyQuestion)
                    .ThenInclude(x => x.Question)
                .Include(x => x.QuestionTypeAnswer)
                .AsQueryable();
        }
        public IQueryable<Response> GetResponsesQuery(int SurveyId)
        {
            return context.Responses
                .Where(x => x.SurveyQuestion.SurveyId == SurveyId)
                .AsQueryable();
        }

        public IEnumerable<Response> GetResponses()
        {
            return GetResponsesQuery()
                .AsEnumerable();
        }
        public IEnumerable<IResponse> GetResponsesBySurveyId(int SurveyId)
        {
            return GetResponsesQuery()
                .Where(x => x.SurveyQuestion.SurveyId == SurveyId);
        }
        public async Task<IEnumerable<Response>> GetResponsesBySurveyIdAsync(int SurveyId)
        {
            return await context.Responses
                .Where(x => x.SurveyQuestion.SurveyId == SurveyId)
                .ToListAsync();
        }
        public async Task<IEnumerable<object>> GetResponsesCustomBySurveyIdAsync(int SurveyId)
        {
            return await GetResponsesQuery()
                .Where(x => x.SurveyQuestion.SurveyId == SurveyId)
                .Select(x => new
                {
                    survey_id = x.SurveyQuestion.Survey.Id,
                    survey_name = x.SurveyQuestion.Survey.Name,
                    question_id = x.SurveyQuestion.Question.Id,
                    question_name = x.SurveyQuestion.Question.Name,
                    question_text = x.SurveyQuestion.Question.Text,
                    answer_id = x.QuestionTypeAnswer.Id,
                    answer = x.QuestionTypeAnswer.Answer,
                    answer_count = x.Count
                })
                .ToListAsync();
        }

        public async Task<int> GetResponsesCountAsync(int SurveyId)
        {
            return await GetResponsesQuery()
                .Where(x => x.SurveyQuestion.SurveyId == SurveyId)
                .SumAsync(x => x.Count);
        }
        public async Task<object> GetResponsesStatsAsync(int SurveyId)
        {
            var query = GetResponsesQuery(SurveyId);
            var questions = new List<object>();

            await query.Select(x => x.SurveyQuestion.Question)
                .Distinct()
                .ForEachAsync(async x =>
            {
                var answers = await query.Where(y => y.SurveyQuestion.QuestionId == x.Id)
                    .Select(a => new
                    {
                        answer_id = a.QuestionTypeAnswer.Id,
                        answer = a.QuestionTypeAnswer.Answer,
                        answer_count = a.Count
                    }).ToListAsync();

                questions.Add(new
                {
                    id = x.Id,
                    name = x.Name,
                    text = x.Text,
                    answers
                });
            });

            return new
            {
                survey_id = SurveyId,
                answer_sum = await query.SumAsync(x => x.Count),
                questions
            };
        }

        public async Task<IResponse> UpdateResponseAsync(int SurveyId, int QuestionId, string Answer)
        {
            try
            {
                var response = GetResponsesQuerySimplified()
                    .FirstOrDefault(x => x.SurveyQuestion.SurveyId == SurveyId &&
                        x.SurveyQuestion.QuestionId == QuestionId &&
                        x.QuestionTypeAnswer.Answer == Answer);

                response.Count++;

                await context.SaveChangesAsync();

                return response;
            }
            catch (Exception e)
            {
                throw new SurveyException(HttpStatusCode.InternalServerError,
                    $"error while updating response. surveyid='{SurveyId}' questionid='{QuestionId}' answer='{Answer}'. exception={e.ToString()}");
            }
        }
        public async Task UpdateResponseAsync(int SurveyId, int QuestionId, string Answer, int Count)
        {
            if (Count > 100 || Count < 0)
                throw new SurveyException(HttpStatusCode.BadRequest,
                    $"count='{Count}' exceeds maximum limit allowed of 100");

            try
            {
                var response = await context.Responses
                    .FirstOrDefaultAsync(x => x.SurveyQuestion.SurveyId == SurveyId &&
                        x.SurveyQuestion.QuestionId == QuestionId &&
                        x.QuestionTypeAnswer.Answer == Answer);

                response.Count += Count;

                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new SurveyException(HttpStatusCode.InternalServerError,
                    $"error while updating response in batch. surveyid='{SurveyId}' questionid='{QuestionId}' answer='{Answer}' count='{Count}'. exception={e.ToString()}");
            }
        }
        public void UpdateResponse(int SurveyId, int QuestionId, string Answer)
        {
            try
            {
                GetResponsesQuerySimplified()
                    .FirstOrDefault(x => x.SurveyQuestion.SurveyId == SurveyId &&
                        x.SurveyQuestion.QuestionId == QuestionId &&
                        x.QuestionTypeAnswer.Answer == Answer)
                    .Count++;

                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new SurveyException(HttpStatusCode.InternalServerError,
                    $"error while updating response. exception={e.ToString()}");
            }
        }


        /*
         * General
         */
        public bool EntityChanged<T>(T Entity) where T : class
        {
            return context.ChangeTracker
                .Entries()
                .FirstOrDefault(x => x.Entity == Entity)
                .State == EntityState.Modified;
        }

        public async Task<T> FindEntityAsync<T>(int Id) where T : class
        {
            return await context.FindAsync<T>(Id);
        }
        public async Task<T> FindEntityAsync<T>(Guid PublicKey) where T : class
        {
            return await context.Set<T>()
                .FirstOrDefaultAsync(x => (x as IPublicKeyId).PublicKey == PublicKey);
        }
        public async Task<T> FindEntityAsync<T>(object PulicKeyId) where T : class
        {
            int Id;
            Guid PublicKey;

            var IsGuid = Guid.TryParse(PulicKeyId.ToString(), out PublicKey);
            var IsInt = int.TryParse(PulicKeyId.ToString(), out Id);

            if (IsGuid)
                return await FindEntityAsync<T>(PublicKey);
            else if (IsInt)
                return await FindEntityAsync<T>(Id);
            else
                return null;
        }
        public object FindEntityAsync(Guid PublicKey)
        {
            return (context.GetType()
                .GetProperties()
                .Where(x => x.Name == "PublicKey"))
                    .FirstOrDefault(x => x.GetValue(PublicKey) != null);
        }

        public async Task<Guid> FindEntityPublicKeyAsync<T>(int Id) where T : class
        {
            return (await FindEntityAsync<T>(Id) as IPublicKeyId)
                .PublicKey;
        }
    }
}
