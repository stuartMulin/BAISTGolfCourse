using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TheBackEndLayer.ViewModels.Login
{
   public class LoginInputModel
    { 

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Email Address:")]
        [StringLength(80)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Wrong email address")]
        [DataType(DataType.Password)]
        [StringLength(400)]
        [MinLength(8, ErrorMessage = "Minimum of 8 characters")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
