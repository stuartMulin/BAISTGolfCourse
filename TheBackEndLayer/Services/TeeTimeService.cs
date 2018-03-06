using TheBackEndLayer.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.ViewModels.Reservation;

namespace TheBackEndLayer.Services
{
    public class TeeTimeService
    {
        public List<TeeTimeViewModel> GetTeeTimesByDate(DateTime searchDate)
        {
            using (var db = new BAISTGolfCourseDbContext())
            {
                var startingBusinessTime = new DateTime(DateTime.Now.Year, searchDate.Month, searchDate.Day, 9, 0, 0);
                var closingBusinessTime = new DateTime(DateTime.Now.Year, searchDate.Month, searchDate.Day, 17, 0, 0);

                var teeTimes = db.TeeTime.Where(x => (x.StartDate > startingBusinessTime &&  
                x.EndDate < closingBusinessTime) && x.TeeState == Enums.TeeTimeStatus.Open && x.Reservations.Count < 4).ToList();

                var teeTimesViewModel = teeTimes.Select(x => new TeeTimeViewModel
                {
                    ID = x.Id,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Status = x.TeeState.ToString(),
                    ReservationCount = x.Reservations.Count
                }).ToList();

                return teeTimesViewModel;
            }
        }
    }
}
