using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace VideoWorld.Repositories
{
    public class ListBasedRepository<T> where T : class
    {
        private readonly List<T> objects;

        public ListBasedRepository()
        {
            objects = new List<T>();
        }

        public ListBasedRepository(Collection<T> entities)
        {
            objects = new List<T>(entities);
        }

        public void Add(T entity)
        {
            if (entity == null)
            {
                throw new Exception();
            }
            objects.Add(entity);
        }

        public void Add(Collection<T> entities)
        {
            if (entities == null)
            {
                throw new Exception();
            }

            if (entities.Any(entity => entity == null))
            {
                throw new Exception();
            }

            objects.AddRange(entities);
        }

            
        public List<T> SelectAll() {
            return new List<T>(objects);
        }
            
        public List<T> SelectAll(Comparer<T> comparator) {
            var result = new List<T>(objects);
            result.Sort(comparator);
            return result;
        }
        
        public List<T> SelectSatisfying(Specification<T> specification) {
            return SelectSatisfyingIntoCollection(specification);
        }
        
        public List<T> SelectSatisfying(Specification<T> specification, Comparer<T> comparator) {
            var result = SelectSatisfyingIntoCollection(specification);
            result.Sort(comparator);
            return result;
        }
        
        public T SelectUnique(Specification<T> specification)  {
            var results = SelectSatisfyingIntoCollection(specification);
            return results.Single();
        }
        
        private List<T> SelectSatisfyingIntoCollection(Specification<T> specification) 
        {
            return new List<T>(objects.Where(arg => specification(arg)));
        }
    }
}