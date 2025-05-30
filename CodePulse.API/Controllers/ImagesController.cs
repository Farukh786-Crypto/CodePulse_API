using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        // GET: {apibaseUrl}/api/images
        [HttpGet]
        public async Task<IActionResult> GetImages()
        {
            // called image repositories  to get all images
            var images = await imageRepository.GetAllImages();
            if (images == null)
            {
                return NotFound("No images found");
            }
            // convert Domain to DTO
            var imagesDTO = images.Select(i => new BlogImageDTO
            {
                Id = i.Id,
                FileName = i.FileName,
                FileExtension = i.FileExtension,
                Title = i.Title,
                Url = i.Url,
                DateCreated = i.DateCreated
            });
            return Ok(imagesDTO);

        }

        // POST: {apibaseUrl}/api/images
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadDTO model)
        {
            ValidateFileUpload(model.File);
            if (ModelState.IsValid)
            {
                // File Upload
                var blogImage = new BlogImage
                {
                    Id = Guid.NewGuid(),
                    FileExtension = Path.GetExtension(model.File.FileName).ToLower(),
                    FileName = model.FileName,
                    Title = model.Title,
                    DateCreated = DateTime.Now
                };
                blogImage = await imageRepository.Upload(model.File,blogImage);

                // convert Domain to DTO
                return Ok(new BlogImageDTO
                {
                    Id = blogImage.Id,
                    FileName = blogImage.FileName,
                    FileExtension = blogImage.FileExtension,
                    Title = blogImage.Title,
                    Url = blogImage.Url,
                    DateCreated = blogImage.DateCreated
                });
            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(IFormFile file)
        {
            if (file != null)
            {
                var allowedExtension = new string[] { ".jpg", ".jpej", ".png" };

                if (!allowedExtension.Contains(Path.GetExtension(file.FileName).ToLower()))
                {
                    ModelState.AddModelError("file", "Unsupported file format");
                }

                if (file.Length > 10485760)// 10MB limit
                {
                    ModelState.AddModelError("file","File size cannot be 10 mb");
                }
            }
        }
    }
}
