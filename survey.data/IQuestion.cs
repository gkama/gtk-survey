using System;
using System.Collections.Generic;
using System.Text;

namespace survey.data
{
    public interface IQuestion<T>
        where T : class
    {
        Guid Id { get; set; }
        IList<object> Responses { get; set; }
    }
}
