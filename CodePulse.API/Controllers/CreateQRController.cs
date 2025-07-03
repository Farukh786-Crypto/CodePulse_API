using CodePulse.API.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using QRCoder;
using System.Drawing;
using System.IO;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateQRController : ControllerBase
    {
        [HttpPost]
        public async Task<QRCodeResponse> CreateQR([FromBody] GenerateQR generateQR)
        {
            try
            {
                bool success = false;
                string message = string.Empty;
                // Create QR Code data
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(generateQR.QRCodeText, QRCodeGenerator.ECCLevel.Q);

                // Generate QR code image
                SvgQRCode qrCode = new SvgQRCode(qrCodeData);
                string svgImage = qrCode.GetGraphic(20);

                // Convert SVG to Base64
                byte[] imageBytes = System.Text.Encoding.UTF8.GetBytes(svgImage);
                string base64Image = Convert.ToBase64String(imageBytes);
                string dataUri = $"data:image/svg+xml;base64,{base64Image}";

                return new QRCodeResponse
                (
                    success: true,
                    message: "QR Code generated successfully.",
                    data: dataUri
                );
                //return Ok(new { qrCodeImage = dataUri }); // json object
            }
            catch
            {
                return new QRCodeResponse(false,
                    "An error occurred while generating the QR code.",
                    string.Empty);
               
            }
        }
    }

    public class QRCodeResponse
    {
        public bool Success { get; init; }
        public string Message { get; init; }
        public string Data { get; set; }

        public QRCodeResponse(bool success,string message,string data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}
