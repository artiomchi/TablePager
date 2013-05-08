using System;
using System.Linq;

namespace FlexLabs.Web.TablePager
{
    public interface IOrderByExpression<T>
    {
        IOrderedQueryable<T> ApplyOrdering(IQueryable<T> query);
        IOrderedQueryable<T> ApplyOrdering(IQueryable<T> query, Boolean ascending);
    }
}
