using TheBackEndLayer.DbModels;
using TheBackEndLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBackEndLayer.ViewModels.Members
{
   public  class MembersViewModel
    {
        [Required]
        public int ID { get; set; }
        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(255)]
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateCreated { get; set; }

        public string PasswordSalt { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }

        public string Gender { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        [Required]
        public string Phone { get; set; }
        public string AlternatePhone { get; set; }
        public string MembershipType { get; set; }
        public ReservationStats ReservationStats { get; set; }
        public AverageScore AverageScore { get; set; }
    }
    public class ReservationStats
    {
        public int ReservationWeek { get; set; }
        public int ReservationMonth { get; set; }
        public int ReservationYear { get; set; }
        public int ReservationAll { get; set; }
    }

    public class AverageScore
    {
        public double ScoreWeek { get; set; }
        public double ScoreMonth { get; set; }
        public double ScoreYear { get; set; }

    }
}
