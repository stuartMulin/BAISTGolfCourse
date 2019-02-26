using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.Enums;

namespace TheBackEndLayer.DbModels
{
  public  class Applicants
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public ApplicantStatus Status { get; set; }
        public RejectionReason RejectionReason { get; set; }
        public Gender Gender { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateCreated { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public bool? HasShareHolderOneConfirmed { get; set; }
        public bool? HasShareHolderTwoConfirmed { get; set; }
        public string Phone { get; set; }
        public string AlternatePhone { get; set; }
        public int ShareHolder1ID { get; set; }
        public int ShareHolder2ID { get; set; }
        public Members ShareHolder1 { get; set; }
        public ICollection<Members> ProspectiveMembers { get; set; }
        public Members ShareHolder2 { get; set; }
    }
}

