using System.ComponentModel.DataAnnotations;

namespace Capstone.Web.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "*")]
        public string Username { get; set; }

        [Required(ErrorMessage = "*")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Two uppercase letters, one of the following: !@#$&*, two numbers, three lowercase letters, and length of 8 ")]
        //[RegularExpression("^(?=.*[A-Z].*[A-Z])(?=.*[!@#$&*])(?=.*[0-9].*[0-9])(?=.*[a-z].*[a-z].*[a-z]).{8}$")]
        public string Password { get; set; }

        [Required(ErrorMessage = "*")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

    }
}