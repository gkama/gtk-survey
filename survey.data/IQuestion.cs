using System;
using System.Collections.Generic;
using System.Text;

namespace survey.data
{
    public interface IQuestion<T>
        where T : IQuestionType
    {
        int Id { get; set; }
        Guid PublicKey { get; set; }
        int TypeId { get; set; }
        string Name { get; set; }
        string Text { get; set; }

        QuestionType Type { get; set; }
    }
}
