using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;

namespace survey.data
{
    public static class ExtensionMethods
    {
        public static T GetObjectById<T>(this IMemoryCache Cache, IQueryable<T> Query, string Key, int Id)
            where T : IPublicKeyId
        {
            return Cache.GetOrCreate<T>(Key, c =>
            {
                c.SlidingExpiration = TimeSpan.FromSeconds(Constants.DefaultCacheSettings);

                return Query
                    .FirstOrDefault<T>(x => x.Id == Id);
            });
        }
    }
}
