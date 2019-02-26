using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using TheBackEndLayer.DbModels;
using EntityState = System.Data.Entity.EntityState;
using System.Linq.Expressions;

namespace TheBackEndLayer.Repositories
{
    public class HoleRepository : GenericRepository<Hole> ,IHoleRepository
    {
        public HoleRepository(DbContext context) : base(context)
        {
        }

     
    }

}
