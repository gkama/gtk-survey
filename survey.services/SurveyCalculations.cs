using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Globalization;

using Microsoft.Extensions.Logging;

using survey.data;

namespace survey.services
{
    public class SurveyCalculations : ISurveyCalculations
    {
        public readonly ILogger log;
        public readonly ISurveyRepository repo;

        public SurveyCalculations(ILogger<SurveyCalculations> log, ISurveyRepository repo)
        {
            this.log = log;
            this.repo = repo;
        }

        public object GetGenericCountFromDate<T>(string Date) where T : class
        {
            try
            {
                var date = DateTime.ParseExact(Date, "yyyy-MM-dd",
                    CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                throw new SurveyException(HttpStatusCode.InternalServerError,
                    $"error while getting generic count from date={Date}. error={e.Message}");
            }

            return this.GetGenericCountFromDate<T>(Date);
        }
        public object GetGenericCountFromDate<T>(DateTime Date) where T : class
        {
            if (typeof(T) == typeof(data.Client))
                return new
                {
                    data = repo.GetClientsQuery()
                    .AsEnumerable()
                    .Where(x => x.Created > Date)
                    .GroupBy(x => x.Created)
                    .Select(x => new
                    {
                        date = x.Key,
                        count = x.Count()
                    })
                };
            else if (typeof(T) == typeof(data.Workspace))
            {
                return new
                {
                    data = repo.GetWorkspacesQuery()
                        .AsEnumerable()
                        .Where(x => x.Created > Date)
                        .GroupBy(x => x.Created)
                        .Select(x => new
                        {
                            date = x.Key,
                            count = x.Count()
                        })
                };
            }
            else if (typeof(T) == typeof(data.Survey))
            {
                return new
                {
                    data = repo.GetSurveysQuery()
                        .AsEnumerable()
                        .Where(x => x.Created > Date)
                        .GroupBy(x => x.Created)
                        .Select(x => new
                        {
                            date = x.Key,
                            count = x.Count()
                        })
                };
            }
            else if (typeof(T) == typeof(data.Question))
            {
                return new
                {
                    data = repo.GetQuestionsQuery()
                        .AsEnumerable()
                        .Where(x => x.Created > Date)
                        .GroupBy(x => x.Created)
                        .Select(x => new
                        {
                            date = x.Key,
                            count = x.Count()
                        })
                };
            }
            else
                throw new SurveyException(HttpStatusCode.InternalServerError,
                    $"error while getting generic count from date. type={typeof(T)} date={Date}");
        }
    }
}
