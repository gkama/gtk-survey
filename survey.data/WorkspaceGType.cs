using System;
using System.Collections.Generic;
using System.Text;

using GraphQL.Types;

namespace survey.data
{
    public class WorkspaceGType : ObjectGraphType<Workspace>
    {
        public WorkspaceGType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Slug);
            Field(x => x.PublicKey, type: typeof(IdGraphType));

            Field<ListGraphType<SurveyGType>>("surveys", resolve: context => context.Source.Surveys);
        }
    }
}
