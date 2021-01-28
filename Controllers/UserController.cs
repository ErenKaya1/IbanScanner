using System;
using System.Threading.Tasks;
using Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.User;

namespace Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IbanScannerUser> _userManager;

        public UserController(UserManager<IbanScannerUser> userManager)
        {
            _userManager = userManager;
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
    }
}