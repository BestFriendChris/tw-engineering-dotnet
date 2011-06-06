using System.Collections.Generic;

namespace VideoWorld.Repositories
{
    public interface IRepository<T>
    {
        void Add(T entity);

        void Add(IList<T> entities);

        List<T> SelectAll();

        List<T> SelectAll(Comparer<T> orderBy);

        List<T> SelectSatisfying(Specification<T> specification);

        List<T> SelectSatisfying(Specification<T> specification, Comparer<T> comparator);

        T SelectUnique(Specification<T> specification);
    }
}