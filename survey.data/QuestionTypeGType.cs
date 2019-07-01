using System;
using System.Collections.Generic;
using System.Text;

using GraphQL.Types;

namespace survey.data
{
    public class QuestionTypeGType : ObjectGraphType<QuestionType>
    {
        public QuestionTypeGType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.PublicKey, type: typeof(IdGraphType));

            Field<ListGraphType<QuestionTypeAnswerGType>>("answers", resolve: context => context.Source.Answers);
        }
    }
}
