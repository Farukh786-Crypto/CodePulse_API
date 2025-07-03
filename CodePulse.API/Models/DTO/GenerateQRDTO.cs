using System.ComponentModel.DataAnnotations;

namespace CodePulse.API.Models.DTO
{
    public class GenerateQRDTO
    {
        [Display(Name = "Enter QRCode Text")]
        public string? QRCodeText { get; set; }
    }
}
