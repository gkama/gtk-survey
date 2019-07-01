﻿using System;
using System.Collections.Generic;
using System.Text;

using GraphQL.Types;

namespace survey.data
{
    public class QuestionGType : ObjectGraphType<Question>
    {
        public QuestionGType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Text);
            Field(x => x.TypeId);
            Field(x => x.PublicKey);

            Field<QuestionTypeGType>("type", resolve: context => context.Source.Type);
        }
    }
}
