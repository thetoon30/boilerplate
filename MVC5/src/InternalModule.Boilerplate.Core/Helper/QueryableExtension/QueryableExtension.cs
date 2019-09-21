using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InternalModule.Boilerplate.Core.Helper
{
    public static class QueryableExtension
    {
        public static QueryableCondition<TSource> If<TSource>(this IQueryable<TSource> queryable, bool condition)
        {
            return new QueryableCondition<TSource>(queryable, condition);
        }
        public static IQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> source, string column, bool type)
        {
            if (string.IsNullOrEmpty(column)) { return source; }
            var expression = source.Expression;
            var parameter = Expression.Parameter(typeof(TSource), "x");
            var selector = Expression.PropertyOrField(parameter, column);
            var method = type ? "OrderBy" : "OrderByDescending";
            expression = Expression.Call(typeof(Queryable), method,
                    new Type[] { source.ElementType, selector.Type },
                    expression, Expression.Quote(Expression.Lambda(selector, parameter)));
            return source.Provider.CreateQuery<TSource>(expression);
        }
    }
}
