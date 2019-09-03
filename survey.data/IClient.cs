using System;
using System.Collections.Generic;
using System.Text;

namespace survey.data
{
    public interface IClient
    {
        int Id { get; set; }
        string Name { get; set; }
        string Slug { get; set; }
        DateTime Created { get; set; }
        DateTime LastUpdated { get; set; }
        int? BillingId { get; set; }
        Guid PublicKey { get; set; }
        ICollection<Workspace> Workspaces { get; }
    }
}
