using System;
using System.Collections.Generic;
using System.Text;

namespace survey.data
{
    public interface INameId
    {
        string Name { get; set; }
        string NameId { get; }
    }
}
