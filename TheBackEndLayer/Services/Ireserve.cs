using System;
using TheBackEndLayer.DbModels;
using TheBackEndLayer.ViewModels.Scores;
using System;
using System.Collections.Generic;
using BAISTGOLF.InViewModels;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.Repositories;
using System.Globalization;
using TheBackEndLayer.InViewModels;
namespace TheBackEndLayer.Services
{
    public interface IreserveService
    {
        CreateInputModel GetInputModel(string name);

        List<TeeTimeViewModel> FindTeeTimes(TeeTimeFinderModel teeTimeFinder);
        bool CreateReservation(ReservationCreateInputModel inputModel, string currentMemberID);
        MemberViewModel AddMemberToReservation(string memberID, int teeTimeID, string currentMemberID);
        List<ReservationViewModel> GetReservations(string email);
    }
}

