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
            Field(x => x.PublicKey);

            Field<SurveyQuestionGType>("survey_question", resolve: context => context.Source.SurveyQuestion);
            Field<QuestionTypeAnswerGType>("question_type_answer", resolve: context => context.Source.QuestionTypeAnswer);
        }
    }
}
