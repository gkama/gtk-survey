using System;
using System.Collections.Generic;
using System.Text;

using GraphQL.Types;

namespace survey.data
{
    public class QuestionTypeAnswerGType : ObjectGraphType<QuestionTypeAnswer>
    {
        public QuestionTypeAnswerGType()
        {
            Field(x => x.Id);
            Field(x => x.TypeId);
            Field(x => x.Answer);
            Field(x => x.PublicKey, type: typeof(IdGraphType));
        }
    }
}
