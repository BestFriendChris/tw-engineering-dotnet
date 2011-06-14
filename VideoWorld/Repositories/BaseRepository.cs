using System;
using System.Collections.Generic;
using System.Linq;

namespace VideoWorld.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly List<T> objects;

        public BaseRepository()
        {
            objects = new List<T>();
        }

        public BaseRepository(IEnumerable<T> entities)
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

        public void Add(IList<T> entities)
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
            return objects;
        }

        public List<T> Select(Func<T, bool> condition)
        {
            return objects.Where(condition).ToList();
        }

        public T SelectUnique(Func<T, bool> condition)
        {
            return objects.SingleOrDefault(condition);
        }

    }
}