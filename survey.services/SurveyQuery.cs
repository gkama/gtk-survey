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
            Field<ListGraphType<SurveyGType>>(
                "surveys",
                resolve: context => repo.GetSurveys());

            Field<SurveyGType>(
                "survey_by_id",
                arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");

                    return repo.GetSurvey(id);
                });

            Field<SurveyGType>(
                "survey_by_publickey",
                arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "publickey" }),
                resolve: context =>
                {
                    var publickey = context.GetArgument<Guid>("publickey");

                    return repo.GetSurvey(publickey);
                });

            Field<ListGraphType<SurveyQuestionGType>>(
                "survey_questions",
                resolve: context => repo.GetSurveyQuestions());
        }
    }
}
