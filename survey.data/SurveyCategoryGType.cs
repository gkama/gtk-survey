using System;
using System.Collections.Generic;
using System.Text;

using GraphQL.Types;

namespace survey.data
{
    public class SurveyCategoryGType : ObjectGraphType<SurveyCategory>
    {
        public SurveyCategoryGType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.NameId);
            Field(x => x.PublicKey, type: typeof(IdGraphType));
        }
    }
}
