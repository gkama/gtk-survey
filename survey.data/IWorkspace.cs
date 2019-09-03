using System;
using System.Collections.Generic;
using System.Text;

namespace survey.data
{
    public interface IWorkspace
    {
        int Id { get; set; }
        string Name { get; set; }
        string Slug { get; set; }
        Guid PublicKey { get; set; }
        ICollection<Survey> Surveys { get; }
    }
}