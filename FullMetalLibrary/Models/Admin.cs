using System.ComponentModel.DataAnnotations;

namespace FullMetalLibrary.Models
{
    public class Admin
    {
        //Primary key
        public int Id { get; set; }

        //Username
        [Required, StringLength(50)] 
        public string UserName { get; set; } = string.Empty;

        //Email
        [Required, EmailAddress, StringLength(100)]
        public string EmailAddress { get; set; } = string.Empty ;

        //Store a hash instead of plaintext for login?
        [Required, StringLength(256)]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; } = string.Empty;

        //Can disable account without deleting it
        public bool IsActive { get; set; } = true;

        //Don't scaffold this in forms
        [ScaffoldColumn(false)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

//Admin is one table where you can see all the approved accounts that you can use to log in. You can edit the password here.
//If you forget your password you're screwed, but that's beyond the scope of this assigment. Hopefully. Richa pls.