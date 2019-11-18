using System;
using System.Collections.Generic;
using System.Text;

namespace survey.services
{
    public interface ISurveyCalculations
    {
        IEnumerable<object> GetGenericCountsFromDate<T>(IEnumerable<DateTime> Dates) where T : class;
        object GetGenericCountFromDate<T>(string Date) where T : class;
        object GetGenericCountFromDate<T>(DateTime Date) where T : class;
    }
}
