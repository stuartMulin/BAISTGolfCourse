using TheBackEndLayer.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBackEndLayer.ViewModels.Reservation
{
   public class ReservationViewModels
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int TeeTimeID { get; set; }
        public DateTime TeeTimeStartDate { get; set; }
        public DateTime TeeTimeEndDate { get; set; }
        public DateTime DateCreated { get; set; }

        public int ID { get; set; }
      
        public TeeTime TeeTime { get; set; }
        
        public string  golfCourse { get; set; }
       
    }
}



