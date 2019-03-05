using TheBackEndLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheBackEndLayer.DbModels
{
    public class Members
    {
        public Members()
        {
            Reservation = new HashSet<Reservations>();
            Scores = new HashSet<PlayerScores>();
        }
       
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
        public int? ApplicantID { get; set; }

        public string PasswordSalt { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }

        public Gender Gender { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public Applicants PreviousApplication { get; set; }
        public ICollection<Applicants> Applicants { get; set; }
        [Required]
        public string Phone { get; set; }
        public string AlternatePhone { get; set; }
        public MembershipStatus MembershipType { get; set; }
        public string MembershipID { get; set; }
        public ICollection<Reservations> Reservation { get; set; }
        public ICollection<PlayerScores> Scores { get; set; }
    }
}