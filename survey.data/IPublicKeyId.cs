using System;
using System.Collections.Generic;
using System.Text;

namespace survey.data
{
    public interface IPublicKeyId
    {
        int Id { get; set; }
        Guid PublicKey { get; set; }
    }
}
