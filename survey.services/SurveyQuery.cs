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
                "survey_by_id",
                arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }),
                resolve: async context =>
                {
                    var id = context.GetArgument<int>("id");

                    return await repo.GetSurveyAsync(id);
                });

            FieldAsync<SurveyGType>(
                "survey_by_publickey",
                arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "publickey" }),
                resolve: async context =>
                {
                    var publickey = context.GetArgument<Guid>("publickey");

                    return await repo.GetSurveyAsync(publickey);
                });

            FieldAsync<ListGraphType<SurveyQuestionGType>>(
                "survey_questions",
                arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "surveyid" }),
                resolve: async context =>
                {
                    var surveyid = context.GetArgument<int>("surveyid");
                    return await repo.GetSurveyQuestionsAsync(surveyid);
                });

            Field<ListGraphType<ResponseGType>>(
                "survey_responses",
                arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "surveyid" }),
                resolve: context =>
                {
                    var surveyid = context.GetArgument<int>("surveyid");

                    return repo.GetResponsesBySurveyId(surveyid);
                });

            FieldAsync<QuestionTypeGType>(
                "question_type",
                arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "name" }),
                resolve: async context =>
                {
                    var name = context.GetArgument<string>("name");

                    return await repo.GetQuestionTypeAsync(name);
                });
        }
    }
}
