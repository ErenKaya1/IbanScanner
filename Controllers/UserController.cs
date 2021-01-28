using System.Collections.Generic;
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
        public IActionResult SignUp(UserRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["SignUpError"] = "Please fill in the required fields";
                return View(model);
            }

            return RedirectToAction("index", "home");
        }
    }
}