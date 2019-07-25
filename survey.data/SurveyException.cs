using System;
using System.Net;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace survey.data
{
    public class SurveyException : ApplicationException
    {
        [JsonProperty("status_code")]
        public int StatusCode { get; set; }

        [JsonProperty("content_type")]
        public string ContentType { get; set; }

        public SurveyException(int StatusCode)
        {
            this.StatusCode = StatusCode;
        }

        public SurveyException(string Message) : base(Message)
        {
            this.StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        public SurveyException(int StatusCode, string Message) : base(Message)
        {
            this.StatusCode = StatusCode;
        }

        public SurveyException(HttpStatusCode StatusCode, string Message) : base(Message)
        {
            this.StatusCode = (int)StatusCode;
        }

        public SurveyException(int StatusCode, Exception Inner) : this(StatusCode, Inner.ToString()) { }
        public SurveyException(HttpStatusCode StatusCode, Exception Inner) : this(StatusCode, Inner.ToString()) { }
        public SurveyException(int StatusCode, JObject ErrorObject) : this(StatusCode, ErrorObject.ToString()) { this.ContentType = @"application/problem+json"; }

    }
}
