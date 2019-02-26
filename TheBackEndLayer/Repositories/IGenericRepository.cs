using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TheBackEndLayer.Repositories
{
  public  interface IgRepository<C> :IDisposable where C: class
    {
        IEnumerable<C> GetAll();
        C GetSingle(int ID);
        IEnumerable<C> FindBy(Expression<Func<C, bool>> predicate);
        void Add(C entity);
        void Delete(C entity);
        void Update(C entity);
        void SaveChanges();
        Task<C> FindByAsync(Expression<Func<C, bool>> predicate);
    }
}
