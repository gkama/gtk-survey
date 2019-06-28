using System;
using System.Collections.Generic;
using System.Text;

namespace survey.data
{
    public interface IResponse
    {
        int Id { get; set; }
        int SurveyId { get; set; }
        int QuestionId { get; set; }
        string Text { get; set; }
        Guid PublicKey { get; set; }
    }
}
