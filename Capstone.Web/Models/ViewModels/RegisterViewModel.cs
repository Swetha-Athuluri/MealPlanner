using System.ComponentModel.DataAnnotations;

namespace Capstone.Web.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username *")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email *")]
        [EmailAddress(ErrorMessage = "Please use a valid email address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password needs 2 numbers, 1 uppercase, 3 lowercase, and be length 8 or greater")]
        //[RegularExpression("^(?=.*[A-Z])(?=.*[0-9].*[0-9])(?=.*[a-z].*[a-z].*[a-z]).{8}$")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password *")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

    }
}