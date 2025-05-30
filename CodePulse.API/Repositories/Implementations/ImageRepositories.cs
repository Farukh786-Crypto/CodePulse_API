using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementations
{
    public class ImageRepositories : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ApplicationDbContext applicationDbContext;

        public ImageRepositories(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            ApplicationDbContext applicationDbContext)

        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<BlogImage>> GetAllImages()
        {
           return await applicationDbContext.BlogImage.ToListAsync();
        }

        public async Task<BlogImage> Upload(IFormFile file, BlogImage blogImage)
        {
            // 1. Save the file to disk
            var localPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images",
                $"{blogImage.FileName}{blogImage.FileExtension}");
            using var stream = new FileStream(localPath, FileMode.Create);
            await file.CopyToAsync(stream);

            // 2. Generate URL and save to database
            var httpRequest = httpContextAccessor.HttpContext.Request;
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{blogImage.FileName}{blogImage.FileExtension}";
            blogImage.Url = urlPath;

            await applicationDbContext.BlogImage.AddAsync(blogImage);
            await applicationDbContext.SaveChangesAsync();

            return blogImage;
        }
    }
}
