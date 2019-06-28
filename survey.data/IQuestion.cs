using System;
using System.Collections.Generic;
using System.Text;

namespace survey.data
{
    public interface IQuestion<T>
        where T : class
    {
        int Id { get; set; }
        Guid PublicKey { get; set; }
        string Name { get; set; }
        IList<IResponse> Responses { get; set; }
    }
}
