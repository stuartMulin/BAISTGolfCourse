using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.ViewModels.ForInputModels;
using TheBackEndLayer.ViewModels.HandlesInPutModels;
using TheBackEndLayer.ViewModels.Members;
using TheBackEndLayer.ViewModels.Reservation;
using TheBackEndLayer.ViewModels.Reservations;

namespace TheBackEndLayer.Services
{
   public interface IReservationService
    {
        List<TeeTimeViewModel> FindTeeTimes(FindTeeTimeModel teeTimeFinder);
        bool CreateReservation(CreateReserveInputModel inputModel, string currentMemberID);
        MembersViewModel AddMemberToReservation(string memberID, int teeTimeID, string currentMemberID);
        List<ReservViewModels> GetReservations(string email);
        void CreateNormalReservation(CreateReservationModel inputModel, int iD);
  
    }
}
