using System;
using System.Collections.Generic;
using System.Text;

using GraphQL.Types;

namespace survey.data
{
    public class SurveyGType : ObjectGraphType<Survey>
    {
        public SurveyGType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Created);
            Field(x => x.CreatedBy);
            Field(x => x.LastUpdated);
            Field(x => x.LastUpdatedBy);
            Field(x => x.PublicKey, type: typeof(IdGraphType));
        }
    }
}
