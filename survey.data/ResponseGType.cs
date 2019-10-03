using System;
using System.Collections.Generic;
using System.Text;

using GraphQL.Types;

namespace survey.data
{
    public class ResponseGType : ObjectGraphType<Response>
    {
        public ResponseGType()
        {
            Field(x => x.Id);
            Field(x => x.Count);
            Field(x => x.SurveyQuestionId);
            Field(x => x.QuestionTypeAnswerId);
            Field(x => x.PublicKey, type: typeof(IdGraphType));

            Field<SurveyQuestionGType>("surveyQuestion", resolve: context => context.Source.SurveyQuestion);
            Field<QuestionTypeAnswerGType>("questionTypeAnswer", resolve: context => context.Source.QuestionTypeAnswer);
        }
    }
}
