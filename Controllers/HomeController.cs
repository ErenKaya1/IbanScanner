using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IbanScanner.Models;
using Models.Iban;
using Utils;
using IronOcr;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace IbanScanner.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(IbanScanViewModel model)
        {
            // Check if it's an image.
            if (!model.ImageFile.IsImage()) return RedirectToAction(nameof(Index));
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images");
            if (!Directory.Exists(imagePath)) Directory.CreateDirectory(imagePath);

            // Upload image
            using (var fs = new FileStream(Path.Combine(imagePath, model.ImageFile.FileName), FileMode.Create))
                await model.ImageFile.CopyToAsync(fs);

            const string ibanRegex = "[a-zA-Z]{2}[0-9]{2}[a-zA-Z0-9]{4}[0-9]{7}([a-zA-Z0-9]?){0,16}";
            var newImagePath = Path.Combine(imagePath, model.ImageFile.FileName);
            var result = new IronTesseract().Read(newImagePath);
            var textResult = result.Text;
            var text = "";

            // Delete Gaps
            for (int i = 0; i < textResult.Length; i++)
            {
                if (textResult[i] == ' ') continue;
                text += textResult[i];
            }

            var match = Regex.Match(text, ibanRegex);
            IbanScanViewModel resultModel;

            if (match.Success)
            {
                resultModel = new IbanScanViewModel
                {
                    Success = true,
                    Iban = match.Value
                };
            }
            else
            {
                resultModel = new IbanScanViewModel
                {
                    Success = false
                };
            }

            return View(resultModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
