using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace TheBackEndLayer.DbModels
{
    public class Login
    {
        [Required(ErrorMessage = "Required")]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Wrong email address")]
        [DataType(DataType.Password)]
        [StringLength(400)]
        [MinLength(8, ErrorMessage = "Minimum of 8 characters")]
        public string Password { get; set; }


    }
}