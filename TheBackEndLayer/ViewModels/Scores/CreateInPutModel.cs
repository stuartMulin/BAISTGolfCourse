using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace TheBackEndLayer.ViewModels.Scores
{
    class CreateInPutModel
    {
        public class CreateInputModel
        {
            public CreateInputModel()
            {
                Holes = new List<HoleViewModel>();
                Handicaps = new List<HandcapViewModel>();
                Reservations = new List<ReserveScoreViewModel>();
            }
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

            public List<HoleViewModel> Holes { get; set; }
            public List<HandcapViewModel> Handicaps { get; set; }
            public List<ReserveScoreViewModel> Reservations { get; set; }
        }

        public class HoleViewModel
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        public class ReserveScoreViewModel
        {
            public int ID { get; set; }
            public string Date { get; set; }
        }

        public class HandcapViewModel
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        public class GCourseViewModel
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public DateTime YearFounded { get; set; }
            public string City { get; set; }
        }
    }
}
