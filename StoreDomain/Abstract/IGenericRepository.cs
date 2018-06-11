using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace GameStore.StoreDomain.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> Get();
        IEnumerable<T> Get(Func<T, bool> predicate);
        T FindById(int id);
        void Create(T item);
        void Update(T item);
        void Remove(T item);
        IEnumerable<T> GetWithInclude(params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> GetWithInclude(Func<T, bool> predicate, 
            params Expression<Func<T, object>>[] includeProperties);
    }
}
