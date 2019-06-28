﻿using System;
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
                    ResponseText = "Yes"
                },
                new Response
                {
                    Id = 2,
                    QuestionId = 1,
                    PublicKey = Guid.NewGuid(),
                    ResponseText = "No"
                },
                new Response
                {
                    Id = 3,
                    QuestionId = 2,
                    PublicKey = Guid.NewGuid(),
                    ResponseText = "Good"
                },
                new Response
                {
                    Id = 4,
                    QuestionId = 2,
                    PublicKey = Guid.NewGuid(),
                    ResponseText = "Awesome! Thanks for asking"
                }
            };
        }
    }
}
