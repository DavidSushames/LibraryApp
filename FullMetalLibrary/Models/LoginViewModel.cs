using System.ComponentModel.DataAnnotations;

namespace FullMetalLibrary.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email or Password")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [RegularExpression(
             @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{8,}$",
             ErrorMessage = "Password must contain uppercase, lowercase, number, special character, and be at least 8 characters long.")]
        public string Password { get; set; } = string.Empty;
    }

}
