using TheBackEndLayer.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.Enums;
using TheBackEndLayer.ViewModels.Members;

namespace TheBackEndLayer.ViewModels.Reservation
{
   public class ReservViewModels

    {
        public ReservViewModels()

        {
            MembersOnReservation = new List<MembersViewModel>();

        }
        public int TeeTimeID { get; set; }
        public DateTime TeeTimeStartDate { get; set; }
        public DateTime TeeTimeEndDate { get; set; }
        public DateTime DateCreated { get; set; }
        public ReservationStatus Status { get; set; }

        public int ID { get; set; }
        public string MemberFullName { get; set; }

        public TeeTime TeeTime { get; set; }
        
        public string  GolfCourse { get; set; }
        public List<MembersViewModel> MembersOnReservation { get; set; }

    }
}



