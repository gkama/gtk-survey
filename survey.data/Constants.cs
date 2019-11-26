using System;
using System.Collections.Generic;
using System.Text;

namespace survey.data
{
    public static class Constants
    {
        public static int DefaultCacheSettings => 60;

        public enum QuestionTypes
        {
            YesNo = 1,
            Semantic = 2,
            OpenEnded = 3,
            Likert = 4
        }
    }
}
