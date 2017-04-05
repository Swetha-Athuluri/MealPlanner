using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email *")]
        [EmailAddress(ErrorMessage = "Please use a valid email address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password *")]
        public string Password { get; set; }

        public string userName { get; set; }

    }
}