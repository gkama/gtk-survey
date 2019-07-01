using System;
using System.Collections.Generic;
using System.Text;

using GraphQL.Types;

namespace survey.data
{
    public class SurveyQuestionGType : ObjectGraphType<SurveyQuestion>
    {
        public SurveyQuestionGType()
        {
            Field(x => x.Id);
            Field(x => x.SurveyId);
            Field(x => x.QuestionId);
            Field(x => x.PublicKey, type: typeof(IdGraphType));

            Field<SurveyGType>("survey", resolve: context => context.Source.Survey);
            Field<QuestionGType>("question", resolve: context => context.Source.Question);
        }
    }
}
