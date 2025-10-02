using System.ComponentModel.DataAnnotations;

namespace FullMetalLibrary.Models
{
    public class RegisterViewModel
    {
        [Required, StringLength(50)]
        public string UserName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8,
            ErrorMessage = "Password must be at least 8 characters long and contain upper, lower, number, and special character.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
            ErrorMessage = "Password must be at least 8 characters long and contain upper, lower, number, and special character.")]
        public string Password { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
