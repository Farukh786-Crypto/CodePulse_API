using System.ComponentModel.DataAnnotations;

namespace CodePulse.API.Models.Domain
{
    public class GenerateQR
    {
        [Display(Name ="Enter QRCode Text")]
        public string? QRCodeText { get; set; }
    }
}
