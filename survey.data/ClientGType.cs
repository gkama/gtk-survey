using System;
using System.Collections.Generic;
using System.Text;

using GraphQL.Types;
namespace survey.data
{
    public class ClientGType : ObjectGraphType<Client>
    {
        public ClientGType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Slug);
            Field(x => x.Created, type: typeof(DateTimeGraphType));
            Field(x => x.LastUpdated, type: typeof(DateTimeGraphType));
            Field(x => x.BillingId, type: typeof(IdGraphType));
            Field(x => x.PublicKey, type: typeof(IdGraphType));

            Field<ListGraphType<WorkspaceGType>>("workspaces", resolve: context => context.Source.Workspaces);
        }
    }
}
