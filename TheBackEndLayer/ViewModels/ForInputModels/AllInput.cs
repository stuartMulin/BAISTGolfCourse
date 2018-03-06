using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using TheBackEndLayer.ViewModels.Scores;
using TheBackEndLayer.DbModels;
using TheBackEndLayer.ViewModels.ReservationScores;
using TheBackEndLayer.Enums;
using TheBackEndLayer.ViewModels.HandlesInPut;
using TheBackEndLayer.ViewModels.HandlesInPutModels;
using TheBackEndLayer.ViewModels.HolebyHole;

namespace TheBackEndLayer.ViewModels.HandlesInPut
{
    public class CreateInputModel
    {
        public CreateInputModel()
        {
            Holes = new List<HoleViewModel>();
            Handcaps = new List<HandicapViewModel>();
            Reservations = new List<ReservationScoreViewModel>();

        }

        [Required (ErrorMessage ="Required")]
        [Display(Name =" Score:")]
        [Range (0,2000, ErrorMessage = "Please enter value greater than 0")]

        public double Score { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Select Reservation: ")]
        public int ReservationID { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Select Hole: ")]
        public int HoleID { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Select Handcap: ")]
        public int HandicapID { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Date Played: ")]
        public DateTime? DatePlayed { get; set; }

        public List<HoleViewModel> Holes{ get; set; }
        public List<HandicapViewModel> Handcaps { get; set; }
        public List<ReservationScoreViewModel> Reservations { get; set; }


    }

    internal class holeViewModel
    {
    }
}
