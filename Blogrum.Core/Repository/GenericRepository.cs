using System;
using System.Linq;
using System.Linq.Expressions;

namespace Blogrum.Core.Repository
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class
    {
        public abstract void Add(T entity);
        public abstract void Update(T entity, int id);
        public abstract void Delete(T entity);
        public abstract T GetByID(object key);
        public abstract IQueryable<T> Query(Expression<Func<T, bool>> predicate);
        public abstract IQueryable<T> GetAll();
        public abstract IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        public int SaveChanges()
        {
            return SaveChanges(true);
        }
        public abstract int SaveChanges(bool validateEntities);
        public abstract void Dispose();
    }
}
