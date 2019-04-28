using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace emed.Models
{
    public class ForgotPW
    {
        [Required(ErrorMessage = "Please enter your email address")]
        [RegularExpression(".+\\@.+\\..+",
        ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        
    }
}