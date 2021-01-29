using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Entity;
using Entity.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.User;
using Service.Contracts;

namespace Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IbanScannerUser> _userManager;
        private readonly IMailService _mailService;

        public UserController(UserManager<IbanScannerUser> userManager, IMailService mailService)
        {
            _userManager = userManager;
            _mailService = mailService;
        }

        [HttpGet("/SignUp")]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost("/SignUp")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(UserRegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new IbanScannerUser
            {
                Id = Guid.NewGuid(),
                UserName = model.Username,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                TempData["SignUpMessage"] = "You can sign in";
                return RedirectToAction("SignIn");
            }
            else return View(model);
        }

        [HttpGet("/SignIn")]
        public IActionResult SignIn(string returnUrl)
        {
            return View();
        }

        [HttpGet("/ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost("/ForgotPassword")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(UserForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var httpEncodedToken = HttpUtility.UrlEncode(resetToken);
                var content = $"<p><a href=\"https://localhost:5001{Url.Action("ResetPassword", "User", new { userId = user.Id, token = httpEncodedToken })}\">Click</a> to reset your password.</p>" + 
                                "<p>This link will expire in 5 minutes.</p>";

                await _mailService.Send(new MailDTO
                {
                    From = _mailService.Username,
                    Subject = "Reset Password",
                    To = new List<string> { model.Email },
                    Content = content
                });
            }

            ViewData["Message"] = "Please check your inbox.";
            return View(model);
        }
    }
}