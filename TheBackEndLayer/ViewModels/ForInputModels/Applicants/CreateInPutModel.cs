using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TheBackEndLayer.ViewModels.ForInputModels.Applicants
{
    class CreateInPutModel
    {
        [Required(ErrorMessage = "Required")]
        [Display(Name = "First Name:")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Last Name:")]
        [StringLength(70)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Email Address:")]
        [StringLength(80)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Remote("CheckifEmailExists", "Account", HttpMethod = "POST", ErrorMessage = "Email already exists")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Password:")]
        [StringLength(600)]
        [MinLength(8, ErrorMessage = "Minimum of 8 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Confirm Password:")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Passwords don't match")]
        [StringLength(600)]
        public string ConfirmPassword { get; set; }

        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Date Of Birth:")]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Address 1:")]
        [StringLength(150)]
        public string Address1 { get; set; }

        [StringLength(150)]
        [Display(Name = "Address 2:")]
        public string Address2 { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(50)]
        [Display(Name = "City:")]
        public string City { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(60)]
        [Display(Name = "Province:")]
        public string Province { get; set; }

        public bool Gender { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(8)]
        [Display(Name = "Postal Code:")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(15)]
        [Display(Name = "Phone:")]
        public string Phone { get; set; }

        [StringLength(50)]
        [Display(Name = "Alternate Phone:")]
        public string AlternatePhone { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "First Shareholder Member ID:")]
        [StringLength(50)]
        [Remote("CheckMembershipNumber1", "Account", HttpMethod = "POST", ErrorMessage = "Membership Number is invalid")]
        public string ShareHolder1MemberID { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Second Shareholder Member ID:")]
        [StringLength(50)]
        [Remote("CheckMembershipNumber2", "Account", HttpMethod = "POST", ErrorMessage = "Membership Number is invalid")]
        public string ShareHolder2MemberID { get; set; }
        public int ShareHolder1ID { get; set; }
        public int ShareHolder2ID { get; set; }
        public List<string> Provinces { get; set; }
    }
}
