using System;
using System.Collections.Generic;
using System.Text;

using GraphQL.Types;

using survey.data;

namespace survey.services
{
    public class SurveyQuery : ObjectGraphType
    {
        public SurveyQuery(ISurveyRepository repo)
        {
            Field<ListGraphType<ClientGType>>(
                "clients",
                resolve: context => repo.GetClients());

            FieldAsync<ClientGType>(
                "client",
                arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }),
                resolve: async context =>
                {
                    var id = context.GetArgument<int>("id");

                    return await repo.GetClientAsync(id);
                });

            Field<ListGraphType<WorkspaceGType>>(
                "workspaces",
                resolve: context => repo.GetWorkspaces());

            FieldAsync<WorkspaceGType>(
                "workspace",
                arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }),
                resolve: async context =>
                {
                    var id = context.GetArgument<int>("id");

                    return await repo.GetWorkspaceAsync(id);
                });


            Field<ListGraphType<SurveyGType>>(
                "surveys",
                resolve: context => repo.GetSurveys());

            FieldAsync<SurveyGType>(
                "surveyById",
                arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }),
                resolve: async context =>
                {
                    var id = context.GetArgument<int>("id");

                    return await repo.GetSurveyAsync(id);
                });

            FieldAsync<SurveyGType>(
                "surveyByPublickey",
                arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "publickey" }),
                resolve: async context =>
                {
                    var publickey = context.GetArgument<Guid>("publickey");

                    return await repo.GetSurveyAsync(publickey);
                });

            Field<ListGraphType<QuestionGType>>(
                "questions",
                resolve: context =>
                {
                    return repo.GetQuestions();
                });

            FieldAsync<QuestionGType>(
                "question",
                arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }),
                resolve: async context =>
                {
                    var id = context.GetArgument<int>("id");

                    return await repo.GetQuestionAsync(id);
                });

            FieldAsync<ListGraphType<SurveyQuestionGType>>(
                "surveyQuestions",
                arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "surveyid" }),
                resolve: async context =>
                {
                    var surveyid = context.GetArgument<int>("surveyid");

                    return await repo.GetSurveyQuestionsAsync(surveyid);
                });

            Field<ListGraphType<ResponseGType>>(
                "surveyResponses",
                arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "surveyid" }),
                resolve: context =>
                {
                    var surveyid = context.GetArgument<int>("surveyid");

                    return repo.GetResponsesBySurveyId(surveyid);
                });

            Field<ListGraphType<QuestionTypeGType>>(
                "questionTypes",
                resolve: context =>
                {
                    return repo.GetQuestionTypes();
                });

            FieldAsync<QuestionTypeGType>(
                "questionType",
                arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "name" }),
                resolve: async context =>
                {
                    var name = context.GetArgument<string>("name");

                    return await repo.GetQuestionTypeAsync(name);
                });
        }
    }
}
