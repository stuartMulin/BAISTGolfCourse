using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.DbModels;
using TheBackEndLayer.ViewModels.Members;
using TheBackEndLayer.ViewModels.Reservation;

namespace TheBackEndLayer.Repositories
{
    public interface IReserveRepository : IgRepository<Reservations>
    {
        List<Reservations> GetReservationsByMemberId(int memberId);
        List<Reservations> GetGeneralListForMember(int MemberiD);
        List<Reservations> GetListReportForMember(int memberID);
        Reservations GetWithGolfCourse(int id);
        List<Reservations> GetNormalListForMember(int memberID);
        List<Reservations> GetListForMember(int memberID);
        List<ReservViewModels> GetMemberReservations(int memberID);
    }

}
