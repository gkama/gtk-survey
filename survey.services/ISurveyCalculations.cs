using System;
using System.Collections.Generic;
using System.Text;

namespace survey.services
{
    public interface ISurveyCalculations
    {
        object GetClientsCountFromDate(DateTime Date);
        object GetGenericCountFromDate<T>(DateTime Date) where T : class;
    }
}
