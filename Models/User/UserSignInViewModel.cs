using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Models.User
{
    [Bind(nameof(Username), nameof(Password), nameof(RememberMe))]
    public class UserSignInViewModel
    {
        [Required]
        [Display(Name = "Username or E-mail")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}