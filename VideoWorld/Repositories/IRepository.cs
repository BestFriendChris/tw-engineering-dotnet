using System;
using System.Collections.Generic;

namespace VideoWorld.Repositories
{
    public interface IRepository<T>
    {
        void Add(T entity);

        void Add(IList<T> entities);

        List<T> SelectAll();

        List<T> Select(Func<T,bool> condition);

        T SelectUnique(Func<T,bool> condition);
    }
}