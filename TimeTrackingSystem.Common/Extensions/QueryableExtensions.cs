using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public static IQueryable<T> SortByProperty<T>(this IQueryable<T> source, string sortColumn, string sortOrder)
        {
            if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortOrder))
            {
                sortColumn = char.ToUpper(sortColumn[0]) + sortColumn.Substring(1);
                if (source.ElementType.GetProperty(sortColumn) != null)
                {
                    if (sortOrder.Equals("asc"))
                    {
                        return source.OrderBy(x => x.GetType().GetProperty(sortColumn).GetValue(x, null));
                    }
                    else
                    {
                        return source.OrderByDescending(x => x.GetType().GetProperty(sortColumn).GetValue(x, null));
                    }
                }
            }
            return source;
        }

    }
}
