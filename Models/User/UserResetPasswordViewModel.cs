using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Models.User
{
    [Bind(nameof(UserId), nameof(ResetToken), nameof(Password), nameof(PasswordConfirm))]
    public class UserResetPasswordViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string ResetToken { get; set; }

        [Required]
        [StringLength(12)]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Password Confirm")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}