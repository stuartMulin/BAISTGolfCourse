using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.DbModels;

namespace TheBackEndLayer.Repositories
{
    public abstract class GenericRepository<C> : IgRepository<C> where C : class
    {

        protected readonly BAISTGolfCourseDbContext DbContext;
        protected readonly IDbSet<C> DbSet;

        public GenericRepository(DbContext context)

        {
            DbContext = context as BAISTGolfCourseDbContext;
            DbSet = DbContext.Set<C>();

        }
        public void Add(C entity)
        {
            DbContext.Set<C>().Add(entity);

        }

        public void Delete(int id)
        {
            C entity = DbSet.Find(id);
            DbSet.Remove(entity);

        }

        IEnumerable<C> IgRepository<C>.FindBy(Expression<Func<C, bool>> predicate)
        {
            return DbContext.Set<C>().Where(predicate).AsEnumerable();
        }

        public Task<C> FindByAsync(Expression<Func<C, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<C> GetAll()
        {
            return DbContext.Set<C>().AsEnumerable();
        }

        public C GetSingle(int ID)
        {
            return DbContext.Set<C>().Find(ID);
        }

        public void Update(C entity)
        {
            try
            {
                var entry = DbContext.Entry(entity);

                DbContext.Set<C>().Attach(entity);
                entry.State = System.Data.Entity.EntityState.Modified;
            }
            catch (System.Data.OptimisticConcurrencyException ex)
            {
                throw ex;
            }

        }
        public C FindBy(Expression<Func<C, bool>> predicate)
        {
            return DbContext.Set<C>().SingleOrDefault(predicate);
        }

        protected virtual void Dispose(bool disposing)
        {
                if (disposing)
                {
                    if (DbContext != null)
                    {
                        DbContext.Dispose();
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
           
        }
        public void SaveChanges()
        {
            try
            {
                DbContext.SaveChanges();

            }
            catch (DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {

                        string message = string.Format("{0}:{1}",

                            validationErrors.Entry.Entity.ToString(),

                            validationError.ErrorMessage);

                        raise = new InvalidOperationException(message, raise);

                    }
                }
                throw raise;
            }
        }

        public void Delete(C entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}