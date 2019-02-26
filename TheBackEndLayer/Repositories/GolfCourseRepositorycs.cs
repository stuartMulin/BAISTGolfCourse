using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.DbModels;

namespace TheBackEndLayer.Repositories
{
   public  class GolfCourseRepository:GenericRepository<GolfCourse>,IGolfCourseRepository
    {
        public GolfCourseRepository(DbContext context) : base(context)
        {
        }

    }
}
