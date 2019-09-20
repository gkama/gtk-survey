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
            Field(x => x.Created);
            Field(x => x.LastUpdated);
            Field(x => x.BillingId);
            Field(x => x.PublicKey, type: typeof(IdGraphType));

            Field<SurveyQuestionGType>("workspaces", resolve: context => context.Source.Workspaces);
        }
    }
}
