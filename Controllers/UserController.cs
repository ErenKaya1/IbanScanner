using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Entity;
using Entity.DTOs;
using Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.User;
using Service.Contracts;
using Utils;

namespace Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IbanScannerUser> _userManager;
        private readonly SignInManager<IbanScannerUser> _signInManager;
        private readonly IMailService _mailService;

        public UserController(UserManager<IbanScannerUser> userManager, IMailService mailService, SignInManager<IbanScannerUser> signInManager)
        {
            _userManager = userManager;
            _mailService = mailService;
            _signInManager = signInManager;
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
        public IActionResult SignIn(string returnUrl = null)
        {
            TempData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("/SignIn")]
        public async Task<IActionResult> SignIn(UserSignInViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            IbanScannerUser user;
            var loginProvider = Utility.GetLoginProvider(model.Username);
            if (loginProvider == LoginProvider.Email) user = await _userManager.FindByEmailAsync(model.Username);
            else user = await _userManager.FindByNameAsync(model.Username);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (TempData["returnUrl"] != null)
                        return Redirect(TempData["returnUrl"].ToString());
                    return RedirectToAction("index", "home");
                }
            }

            ViewData["SignInError"] = "Please check the information.";
            return View(model);
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

        [HttpGet("/ResetPassword")]
        public IActionResult ResetPassword(string userId, string token)
        {
            var model = new UserResetPasswordViewModel
            {
                UserId = userId,
                ResetToken = HttpUtility.UrlDecode(token),
            };

            return View(model);
        }

        [HttpPost("/ResetPassword")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(UserResetPasswordViewModel model)
        {
            if (!ModelState.IsValid || model.Password != model.PasswordConfirm) return View(model);

            var user = await _userManager.FindByIdAsync(model.UserId.ToString());
            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, model.ResetToken, model.Password);
                if (result.Succeeded)
                {
                    TempData["ResetPasswordMessage"] = "You can login with new password.";
                    return RedirectToAction(nameof(SignIn));
                }
            }

            ViewData["ResetPasswordError"] = Messages.DEFAULT_ERROR_MESSAGE;
            return View(model);
        }

        [HttpGet("/SignOut")]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
    }
}