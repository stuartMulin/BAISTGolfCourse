using TheBackEndLayer.DbModels;
using TheBackEndLayer.ViewModels.HandlesInPutModels;
using TheBackEndLayer.ViewModels.HolebyHole;
using TheBackEndLayer.ViewModels.ReservationScores;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BAISTGOLF.InViewModels
{
    public class CreatePlayerScore

    {
        public CreatePlayerScore()
        {
            Holes = new List<HoleViewModel>();
            Handicaps = new List<HandicapViewModel>();
            Reservations = new List<ReservationScoreViewModel>();

        }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Enter Member ID: ")]
        public string MemberID { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Enter Score: ")]
        [Range(0, 3000, ErrorMessage = "The value must be greater than 0")]
        public double Score { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Select Reservation: ")]
        public int ReservationID { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Select Hole: ")]
        public int HoleID { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Select Handicap: ")]
        public int HandicapID { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Date Played: ")]
        public DateTime? DatePlayed { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Select Golf Course: ")]
        public int GolfCourseID { get; set; }

        public List<HoleViewModel> Holes { get; set; }
        public List<HandicapViewModel> Handicaps { get; set; }
        public List<ReservationScoreViewModel> Reservations { get; set; }
    }
}