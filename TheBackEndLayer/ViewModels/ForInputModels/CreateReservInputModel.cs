using System.Collections.Generic;
using TheBackEndLayer.DbModels;
using System.ComponentModel.DataAnnotations;

namespace TheBackEndLayer.ViewModels.HandlesInPutModels
{
    public class CreateReserveInputModel
    {

        public  CreateReserveInputModel()
        {
          
            Reservations = new List<ReservationsModel>();
        }

        [Required (ErrorMessage ="This field is required")]
        public int TeetimeId { get; set; }
        public List<ReservationsModel> Reservations { get; set; }

    }

    public class ReservationsModel
    {
        public int MemberId { get; set; }
        public int CurrentStatus { get; set; }
    }
}
