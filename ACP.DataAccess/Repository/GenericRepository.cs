using ACP.Business.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ACP.DataAccess.Repository
{
    public abstract class GenericRepository<TContext> : IGenericRepository
    where TContext : DbContext, new()
    {
        private TContext _context;

        public TContext Context
        {
            get { return _context; }
            set { _context = value; }
        }

        private bool _disposed;

        protected GenericRepository()
        {
            _context = new TContext();
            //disabled lazy loading. Include properties must be specified in Get methods
            _context.Configuration.LazyLoadingEnabled = false;
        }

        public IQueryable<T> All<T>() where T : class
        {
            return _context.Set<T>();
        }

        public IQueryable<T> AllIncluding<T>(params Expression<Func<T, object>>[] include) where T : class
        {
            IQueryable<T> query = _context.Set<T>();

            return include.Aggregate(query, (current, item) => current.Include(item));
        }       

        public T GetSingle<T>(Func<T, bool> where, params Expression<Func<T, object>>[] include) where T : class
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var entity in include)
            {
                query = query.Include(entity);
            }

            return query.FirstOrDefault(where);
        }

        public async Task<T> GetSingleAsync<T>(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] include) where T : class
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var entity in include)
            {
                query = query.Include(entity);
            }

            return await query.FirstOrDefaultAsync(where);
        }


        public T Add<T>(T item) where T : class
        {
            try
            {
                _context.Set<T>().Add(item);
            }
            catch (DbEntityValidationException)
            {
                throw;
            }
            return item;
        }

        public T Update<T>(T entity) where T : class
        {
            try
            {
                var entry = _context.Entry(entity);
                _context.Set<T>().Attach(entity);
                entry.State = System.Data.Entity.EntityState.Modified;
            }
            catch (DbEntityValidationException)
            {
                throw;
            }

            return entity;
        }

        public int Delete<T>(T entity) where T : class
        {
            int retVal = 0;
            try
            {
                if (_context.Entry(entity).State == System.Data.Entity.EntityState.Detached)
                {
                    _context.Set<T>().Attach(entity);
                }
                _context.Set<T>().Remove(entity);
            }
            catch (DbEntityValidationException)
            {
                throw;
            }
            return retVal;
        }

        public virtual T[] AddMany<T>(params T[] entities) where T : class
        {
            try
            {
                foreach (T item in entities)
                {
                    _context.Set<T>().Add(item);
                }
            }
            catch (DbEntityValidationException)
            {
                throw;
            }
            return entities;
        }

        public T[] UpdateMany<T>(params T[] entities) where T : class
        {
            try
            {
                foreach (T item in entities)
                {
                    var entry = _context.Entry(item);
                    _context.Set<T>().Attach(item);
                    entry.State = System.Data.Entity.EntityState.Modified;
                }
            }
            catch (DbEntityValidationException)
            {
                throw;
            }
            return entities;
        }

        public int DeleteMany<T>(params T[] entities) where T : class
        {
            int retVal = 0;
            try
            {
                foreach (T item in entities)
                {
                    _context.Set<T>().Remove(item);
                }
            }
            catch (DbEntityValidationException)
            {
                throw;
            }
            return retVal;
        }

        public int Commit()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (DbUpdateException ex) //DbContext
            {
                Trace.TraceError(ex.Message);

                Exception innerException = ex;
                while (innerException.InnerException != null) innerException = innerException.InnerException;

                throw innerException;
            }
            catch (UpdateException ex)
            {
                Trace.TraceError(ex.Message);
                Exception innerException = ex;

                while (innerException.InnerException != null) innerException = innerException.InnerException;

                throw innerException;
            }

        }

        public async Task<int> CommitAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) //DbContext
            {
                Trace.TraceError(ex.Message);
                Exception innerException = ex;

                while (innerException.InnerException != null) innerException = innerException.InnerException;

                throw innerException;
            }
            catch (UpdateException ex)
            {
                Trace.TraceError(ex.Message);
                Exception innerException = ex;

                while (innerException.InnerException != null) innerException = innerException.InnerException;

                throw innerException;
            }

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }


    }
}
