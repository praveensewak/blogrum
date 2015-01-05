using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;

namespace Blogrum.Core.Repository
{
    public class MainRepository<T> : GenericRepository<T> where T : class
    {
        protected BlogrumDbContext Context;
        protected DbSet<T> DbSet;

        public MainRepository()
        {
            Context = EntityContext.GetContext();
            DbSet = Context.Set<T>();
        }

        public MainRepository(BlogrumDbContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }

        public override void Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            DbSet.Add(entity);
        }

        public override void Update(T entity, int id)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            var entry = Context.Entry(entity);

            if (entry.State == System.Data.Entity.EntityState.Detached)
            {
                var attachedEntity = DbSet.Find(id); // Need to have access to key

                if (attachedEntity != null)
                {
                    var attachedEntry = Context.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    entry.State = System.Data.Entity.EntityState.Modified; // This should attach entity
                }
            }
        }

        public override void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            var entry = Context.Entry(entity);
            entry.State = System.Data.Entity.EntityState.Deleted;
        }

        public override T GetByID(object key)
        {
            return DbSet.Find(key);
        }

        public override IQueryable<T> Query(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public override IQueryable<T> GetAll()
        {
            return Context.Set<T>();
        }

        public override IQueryable<T> GetAllIncluding(params System.Linq.Expressions.Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public override int SaveChanges(bool validateEntities)
        {
            Context.Configuration.ValidateOnSaveEnabled = validateEntities;
            return Context.SaveChanges();
        }

        #region IDisposable implementation

        public override void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
                GC.SuppressFinalize(this);
            }
        }

        #endregion
    }
}
