using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FlexLabs.Web.TablePager
{
    public class OrderByExpressionCollection<TKey, TModel> : IEnumerable
    {
        private Dictionary<TKey, IOrderByExpression<TModel>> storage = new Dictionary<TKey, IOrderByExpression<TModel>>();

        public void Add<R>(TKey key, Expression<Func<TModel, R>> expression)
        {
            storage.Add(key, new OrderByExpression<TModel, R>(expression));
        }

        public IOrderByExpression<TModel> this[TKey key]
        {
            get
            {
                return storage[key];
            }
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
