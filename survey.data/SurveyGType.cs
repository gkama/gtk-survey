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
            Field(x => x.Created, type: typeof(DateTimeGraphType));
            Field(x => x.CreatedBy);
            Field(x => x.LastUpdated, type: typeof(DateTimeGraphType));
            Field(x => x.LastUpdatedBy);
            Field(x => x.WorkspaceId, type: typeof(IdGraphType));
            Field(x => x.PublicKey, type: typeof(IdGraphType));

            Field<SurveyCategoryGType>("category", resolve: context => context.Source.Category);
            Field<WorkspaceGType>("workspace", resolve: context => context.Source.Workspace);
        }
    }
}
