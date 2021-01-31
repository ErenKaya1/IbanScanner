using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Models.Iban
{
    [Bind(nameof(ImageFile))]
    public class IbanScanViewModel
    {
        [Required]
        [Display(Name = "Upload Image")]
        public IFormFile ImageFile { get; set; }
    }
}