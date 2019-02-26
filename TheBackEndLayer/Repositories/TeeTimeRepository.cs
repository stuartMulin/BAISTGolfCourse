using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.DbModels;

namespace TheBackEndLayer.Repositories
{
    public class TeeTimeRepository : GenericRepository<TeeTime>, ITeeTimeRepository
    {
        
        public TeeTimeRepository(DbContext context) : base(context)
        {
        }


        public TeeTime GetWithReservationsById(int id)
        {
            return DbContext.TeeTime
                .Include(x => x.Reservations)
                .SingleOrDefault(x => x.Id == id);
        }
        public List<TeeTime> GetList(DateTime startDate, DateTime endDate)
        {
            return DbSet.Include(x => x.Reservations)
                        .Where(x => x.StartDate > DateTime.Now && (x.StartDate >= startDate)
                        && (x.EndDate <= endDate) && x.Reservations.Count < 4)
                        .OrderBy(x => x.StartDate).ToList();
        }
        public TeeTime GetWithMembers(int id)
        {
            return DbSet.Include(x => x.Reservations.Select(t => t.Member))
                        .SingleOrDefault(x => x.Id == id);
        }

    
        
    }
}
