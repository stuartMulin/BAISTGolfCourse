using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using TheBackEndLayer.DbModels;
using TheBackEndLayer.ViewModels.Members;
using EntityState = System.Data.Entity.EntityState;

namespace TheBackEndLayer.Repositories
{
    public class ReserveRepository: GenericRepository<Reservations>, IReserveRepository 
    {
        
        public ReserveRepository(DbContext context): base(context)
        {
        }

        public List<Reservations> GetReservationsByMemberId(int memberId)
        {
            return DbSet.Include(x => x.TeeTime).Where(x => x.MemberID == memberId
            && x.TeeTime.StartDate > DateTime.UtcNow).ToList();
        }
        public List<Reservations> GetListForMember(int memberID)
        {
            return DbSet.Include(x => x.TeeTime).Where(x => x.MemberID == memberID
            && x.TeeTime.StartDate > DateTime.UtcNow).ToList();
        }
        public List<Reservations> GetGeneralListForMember(int memberiD)
        {
            return DbSet.Include(x => x.TeeTime).Where(x => x.MemberID == memberiD).ToList();
        }
        public Reservations GetWithGolfCourse(int id)
        {
            return DbSet.Include(x => x.TeeTime.GolfCourse).Where(x => x.ID == id).SingleOrDefault();
        }
        public List<Reservations> GetNormalListForMember(int memberID)
        {
            return DbSet.Include(x => x.TeeTime).Where(x => x.MemberID == memberID).ToList();
        }
        public List<Reservations> GetListReportForMember(int memberID)
        {

            return DbSet.Include(x => x.TeeTime).ToList();
        }
    }
}
