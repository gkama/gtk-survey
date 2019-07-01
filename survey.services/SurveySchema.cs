using System;
using System.Collections.Generic;
using System.Text;

using GraphQL;
using GraphQL.Types;

namespace survey.services
{
    public class SurveySchema : Schema
    {
        public SurveySchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<SurveyQuery>();
        }
    }
}
