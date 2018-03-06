
using TheBackEndLayer.DbModels;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using TheBackEndLayer.ViewModels.Reservation;
using TheBackEndLayer.Services;

namespace TheBackEndLayer.Services
{
    public class ReservationService
    {
        public List<ReservationViewModels> GetReservations(int memberId)
        {

            using (var db = new BAISTGolfCourseDbContext())
            {
                var reservations = db.Reservations
                    .Include(x => x.Member)
                    .Include(x => x.TeeTime.GolfCourse)
                    .Where(x => x.MemberID == memberId).ToList();

                if (reservations.Count > 0)
                {
                    var listOfReservations = new List<ReservationViewModels>();

                    foreach (var reservation in reservations)
                    {
                        var reservationModel = PopulateViewModel(reservation);
                        listOfReservations.Add(reservationModel);
                    }

                    return listOfReservations;
                }
                else
                {
                    return null;

                }

            }
        }

        public ReservationViewModels GetById(int id)
        {
            using (var db = new BAISTGolfCourseDbContext())
            {
                var reservation = db.Reservations.Include(x => x.Member).SingleOrDefault(x => x.ID == id);

                return PopulateViewModel(reservation);
            }

        }

        private ReservationViewModels PopulateViewModel(Reservations reservation)
        {
            var reservationViewModel = new ReservationViewModels
            {
                DateCreated = reservation.DateCreated,
                TeeTimeID = reservation.TeeTimeID,
                golfCourse = reservation.TeeTime.GolfCourse.CourseName,
                TeeTimeStartDate = reservation.TeeTime.StartDate,
                TeeTimeEndDate = reservation.TeeTime.EndDate,
                FirstName = reservation.Member.FirstName,
                LastName = reservation.Member.LastName
            };

            return reservationViewModel;

        }

        public bool CreateReservation(CreateReservationModel model, int memberID)
        {
            var successful = false;

            try
            {
                //TODO: Send email to members added 

                using (var db = new BAISTGolfCourseDbContext())
                {
                    
                    AddCurrentUserToReservation(model, memberID);

                    foreach (var member in model.PotentialReservations)
                    {
                        var reservation = new Reservations
                        {
                            DateCreated = DateTime.Now,
                            TeeTimeID = model.TeeTimeID,
                            MemberID = member.MemberID,
                            Status = member.Status,
                            IsApproved = false,
                            Type = Enums.ReservationType.Normal
                        };

                        db.Reservations.Add(reservation);
                    }

                    var reservationCount = model.PotentialReservations.Count;

                    if (reservationCount == 4)
                    {
                        var teeTime = db.TeeTime.SingleOrDefault(x => x.Id == model.TeeTimeID);
                        teeTime.TeeState = Enums.TeeTimeStatus.Closed;
                    }

                    db.SaveChanges();
                }

                successful = true;
                return successful;
            }
            catch
            {
                return successful;
            }

           
        }

        public void AddCurrentUserToReservation(CreateReservationModel model, int memberID)
        {
            model.PotentialReservations.Add(new ViewModels.Reservations.ReservationCreateModel { MemberID = memberID, Status = Enums.ReservationStatus.Accepted });
        }
    }

    
}
