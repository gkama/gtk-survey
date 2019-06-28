using System;
using System.Collections.Generic;
using System.Text;

namespace survey.data
{
    public interface IResponse
    {
        int Id { get; set; }
        int Count { get; set; }
        int QuestionId { get; set; }
        int QuestionTypeAnswerId { get; set; }
        Guid PublicKey { get; set; }
    }
}
