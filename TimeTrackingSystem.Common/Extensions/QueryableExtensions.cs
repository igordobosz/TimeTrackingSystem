using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeTrackingSystem.Common.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paged<T>(this IQueryable<T> source, int pageIndex,
            int pageSize)
        {
            return source
                .Skip(pageIndex * pageSize)
                .Take(pageSize);
        }
    }
}
