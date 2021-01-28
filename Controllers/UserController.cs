using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    public class UserController : Controller
    {
        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false)]
        [HttpGet("/SignUp")]
        public IActionResult SignUp()
        {
            return View();
        }
    }
}