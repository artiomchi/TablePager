using System;
using System.Linq;
using System.Linq.Expressions;

namespace FlexLabs.Web.TablePager
{
    // Derived from http://forums.microsoft.com/MSDN/ShowPost.aspx?PostID=2641510&SiteID=1
    public class OrderByExpression<T, U> : IOrderByExpression<T>
    {
        private Expression<Func<T, U>> exp = null;
        public OrderByExpression(Expression<Func<T, U>> expression)
        {
            exp = expression;
        }
        public IOrderedQueryable<T> ApplyOrdering(IQueryable<T> query)
        {
            return query.OrderBy(exp);
        }
        public IOrderedQueryable<T> ApplyOrdering(IQueryable<T> query, Boolean ascending)
        {
            if (ascending)
                return query.OrderBy(exp);
            else
                return query.OrderByDescending(exp);
        }
    }
}
