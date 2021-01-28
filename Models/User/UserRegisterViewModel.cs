using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Models.User
{
    [Bind(nameof(Username), nameof(Email), nameof(Password), nameof(PasswordConfirm))]
    public class UserRegisterViewModel
    {
        [Required]
        [StringLength(12)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Password Confirm")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}