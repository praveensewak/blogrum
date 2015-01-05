using System;
using System.Linq;
using System.Linq.Expressions;

namespace Blogrum.Core.Repository
{
    public partial interface IRepository<T> : IDisposable where T : class
    {
        void Add(T entity);
        void Update(T entity, int id);
        void Delete(T entity);
        T GetByID(object key);
        IQueryable<T> Query(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        int SaveChanges();
        int SaveChanges(bool validateEntities);
    }
}
