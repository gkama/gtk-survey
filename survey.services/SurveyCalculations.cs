using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

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

        public IEnumerable<object> GetGenericCountsFromDate<T>(IEnumerable<DateTime> Dates) where T : class
        {
            var counts = new List<object>();

            foreach (var d in Dates)
                counts.Add(GetGenericCountFromDate<T>(d));

            return counts
                .AsEnumerable();
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
            if (typeof(T) == typeof(Client))
                return new
                {
                    data = GetIQueryable<Client>()
                        .AsEnumerable()
                        .Where(x => x.Created > Date)
                        .GroupBy(x => x.Created)
                        .Select(x => new
                        {
                            date = x.Key,
                            count = x.Count()
                        })
                };
            else if (typeof(T) == typeof(Workspace))
            {
                return new
                {
                    data = GetIQueryable<Workspace>()
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
            else if (typeof(T) == typeof(Survey))
            {
                return new
                {
                    data = GetIQueryable<Survey>()
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
            else if (typeof(T) == typeof(Question))
            {
                return new
                {
                    data = GetIQueryable<Question>()
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

        public IQueryable<T> GetIQueryable<T>() where T : class
        {
            if (typeof(T) == typeof(Client))
                return (IQueryable<T>)repo.GetClientsQuery();
            else if (typeof(T) == typeof(Workspace))
                return (IQueryable<T>)repo.GetWorkspacesQuery();
            else if (typeof(T) == typeof(Survey))
                return (IQueryable<T>)repo.GetSurveysQuery();
            else if (typeof(T) == typeof(Question))
                return (IQueryable<T>)repo.GetQuestionsQuery();
            else
                throw new SurveyException(HttpStatusCode.InternalServerError,
                        $"error while getting IQueryable<T>. type={typeof(T)}");
        }

        public string GetDistinctDatesAsString<T>() where T : class
        {
            return JsonSerializer.Serialize(GetDistinctDates<T>());
        }
        public object GetDistinctDates<T>() where T : class
        {
            if (typeof(T) == typeof(Client))
                return new
                {
                    data = GetIQueryable<Client>()
                        .AsEnumerable()
                        .Select(x => x.Created.Date)
                        .Distinct()
                };
            else if (typeof(T) == typeof(Workspace))
                return new
                {
                    data = GetIQueryable<Workspace>()
                        .AsEnumerable()
                        .Select(x => x.Created.Date)
                        .Distinct()
                };
            else if (typeof(T) == typeof(Survey))
                return new
                {
                    data = GetIQueryable<Survey>()
                        .AsEnumerable()
                        .Select(x => x.Created.Date)
                        .Distinct()
                };
            else if (typeof(T) == typeof(Question))
                return new
                {
                    data = GetIQueryable<Question>()
                        .AsEnumerable()
                        .Select(x => x.Created.Date)
                        .Distinct()
                };
            else
                throw new SurveyException(HttpStatusCode.InternalServerError,
                    $"error while getting distinct dates. type={typeof(T)}");
        }
    }
}
