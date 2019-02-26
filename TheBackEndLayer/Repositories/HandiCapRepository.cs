using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.DbModels;

namespace TheBackEndLayer.Repositories
{
    public class HandiCapRepository : GenericRepository<HandiCap>, IHandiCapRepository
    {
        public HandiCapRepository(DbContext context) : base(context)
        {

        }
}
}
