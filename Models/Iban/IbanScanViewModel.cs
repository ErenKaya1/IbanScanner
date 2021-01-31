using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Models.Iban
{
    public class IbanScanViewModel
    {
        [Required]
        [Display(Name = "Upload Image")]
        public IFormFile ImageFile { get; set; }

        public bool Success { get; set; }
        public string Iban { get; set; }
    }
}