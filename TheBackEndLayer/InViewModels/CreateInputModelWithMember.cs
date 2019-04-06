using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TheBackEndLayer.InViewModels
{
    public  class CreateInputModelWithMember
    {

        public CreateInputModelWithMember()
        {
            HoleEntries = new List<HoleEntryViewModel>();
            Handicaps = new List<HandicapViewModel>();
            Reservations = new List<ReservationScoreViewModel>();

        }
        //[Required(ErrorMessage = "Required")]
        [Display(Name = "Enter Member ID: ")]
        [Remote("CheckMemberNumberExists", "Account", HttpMethod = "POST", ErrorMessage = "Member ID does not exist")]
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
        public DateTime DatePlayed { get; set; }
        public List<HoleEntryViewModel> HoleEntries { get; set; }
        public List<HandicapViewModel> Handicaps { get; set; }
        public List<ReservationScoreViewModel> Reservations { get; set; }
    }

    public class HoleEntryViewModel
    {
        public int HoleID { get; set; }
        public string HoleName { get; set; }
        public int Score { get; set; }
    }
}
