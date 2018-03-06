using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheBackEndLayer.ViewModels.HandlesInPutModels
{
    public class ForAllReservations
    {

        public  ForAllReservations()
        {
          
            Reservations = new List<Reservations>();
        }

        [Required (ErrorMessage ="This field is required")]
        public int TeetimeId { get; set; }
        public List<Reservations> Reservations { get; set; }

    }

    public class Reservations
    {
        public int MemberId { get; set; }
        public int CurrentStatus { get; set; }
    }
}
