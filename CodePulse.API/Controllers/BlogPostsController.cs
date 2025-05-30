using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepositories blogPostRepositories;
        private readonly ICategorirsRepositories categorirsRepositories;

        public BlogPostsController(IBlogPostRepositories blogPostRepositories, ICategorirsRepositories categorirsRepositories)
        {
            this.blogPostRepositories = blogPostRepositories;
            this.categorirsRepositories = categorirsRepositories;
        }

        [HttpGet]
        public async Task<FetchBlogPostResponse> CreatePosts()
        {
            string message = string.Empty;
            bool success = false;
            string data = string.Empty;
            var blogPosts = await blogPostRepositories.getAllBlogPost();

            // convert model to DTO
            var responseData = new List<BlogPostDTO>();
            foreach (var blogpost in blogPosts)
            {
                responseData.Add(new BlogPostDTO
                {
                    Id = blogpost.Id,
                    Author = blogpost.Author,
                    Content = blogpost.Content,
                    FeaturedImageUrl = blogpost.FeaturedImageUrl,
                    IsVisible = blogpost.IsVisible,
                    PublishedDate = blogpost.PublishedDate,
                    shortDescription = blogpost.shortDescription,
                    Title = blogpost.Title,
                    UrlHandle = blogpost.UrlHandle,
                    categories = blogpost.Categories.Select(c => new CategoryDTO
                    {
                        Id = c.Id,
                        Name = c.Name,
                        UrlHandle = c.UrlHandle
                    }).ToList()
                });
                success = true;
                message = $"Data fetched successfully.";
            }
            return new FetchBlogPostResponse(success, message, responseData);

        }

        [HttpGet]
        [Route("{BlogPostId:Guid}")]
        public async Task<BlogPostSingleResponse> GetPostById([FromRoute] Guid BlogPostId)
        {
            bool success = false;
            string message = string.Empty;
            string data = string.Empty;

            var ExitstBlofPost = await blogPostRepositories.getBlogPostByID(BlogPostId);
            if (ExitstBlofPost is null)
            {
                message = "BlofPost Not Found";
                return new BlogPostSingleResponse(success, message, new BlogPostDTO());
            }
            var responseData = new BlogPostDTO()
            {
                Id = ExitstBlofPost.Id,
                Author = ExitstBlofPost.Author,
                categories = new List<CategoryDTO>(),
                Content = ExitstBlofPost.Content,
                FeaturedImageUrl = ExitstBlofPost?.FeaturedImageUrl,
                IsVisible = ExitstBlofPost.IsVisible,
                PublishedDate = ExitstBlofPost.PublishedDate,
                shortDescription = ExitstBlofPost.shortDescription,
                Title = ExitstBlofPost.Title,
                UrlHandle = ExitstBlofPost.UrlHandle
            };

            responseData.categories = ExitstBlofPost.Categories?
                .Where(c => c != null).Select(c => new CategoryDTO
                {
                    Id = c.Id,
                    UrlHandle = c.UrlHandle,
                    Name = c.Name
                }).ToList() ?? new List<CategoryDTO>();


            //foreach (var category in ExitstBlofPost.Categories)
            //{
            //    if (category is not null)
            //    {
            //        responseData.categories.Add(new CategoryDTO
            //        {
            //            CategoryId = category.Id,
            //            Name = category.Name,
            //            UrlHandle = category.UrlHandle
            //        });
            //    }
            //}

            success = true;
            message = "Blog fetch Succesfully !";

            return new BlogPostSingleResponse(success, message, responseData);

        }

        // GET : {apiBaseUrl}/api/BlogPosts/{urlhandle}
        [HttpGet]
        [Route("{urlHandle}")]
        public async Task<BlogPostSingleResponse> GetBlogPostByUrlHandle([FromRoute] string urlHandle)
        {
            bool success = false;
            string message = string.Empty;
            string data = string.Empty;
            var blogPost = await blogPostRepositories.getBlogPostByUrlHandle(urlHandle);
            if (blogPost is null)
            {
                message = "BlofPost Not Found";
                return new BlogPostSingleResponse(success, message, new BlogPostDTO());
            }
            var responseData = new BlogPostDTO()
            {
                Id = blogPost.Id,
                Author = blogPost.Author,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                IsVisible = blogPost.IsVisible,
                PublishedDate = blogPost.PublishedDate,
                shortDescription = blogPost.shortDescription,
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle
            };
            success = true;
            message = "Blog fetch Succesfully !";
            return new BlogPostSingleResponse(success, message, responseData);
        }

        [HttpPost]
        [Authorize(Roles = IdentityDefaults.WriterRole)]
        public async Task<BlogPostResponse> AddBlogPost([FromBody] AddBlogPosts blogPostDTO)
        {
            string message = string.Empty;
            bool success = false;
            // convert DTO to domain
            var BlogPost = new BlogPost()
            {
                Id = blogPostDTO.Id,
                Author = blogPostDTO.Author,
                Content = blogPostDTO.Content,
                FeaturedImageUrl = blogPostDTO.FeaturedImageUrl,
                IsVisible = blogPostDTO.IsVisible,
                PublishedDate = blogPostDTO.PublishedDate,
                shortDescription = blogPostDTO.shortDescription,
                Title = blogPostDTO.Title,
                UrlHandle = blogPostDTO.UrlHandle,
                Categories = new List<Category>()
            };

            foreach (var categoryDTO in blogPostDTO.categories)
            {
                var existCategory = await categorirsRepositories.GetById(categoryDTO);
                if (existCategory is not null)
                {
                    BlogPost.Categories.Add(existCategory);
                }
            }

            await blogPostRepositories.blogPostAsync(BlogPost);
            var response = new BlogPostDTO()
            {
                UrlHandle = BlogPost.UrlHandle,
                Title = BlogPost.Title,
                Author = BlogPost.Author,
                Content = BlogPost.Content,
                FeaturedImageUrl = BlogPost.FeaturedImageUrl,
                Id = BlogPost.Id,
                IsVisible = BlogPost.IsVisible,
                PublishedDate = BlogPost.PublishedDate,
                shortDescription = BlogPost.shortDescription,
                categories = BlogPost.Categories.Select(c => new CategoryDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    UrlHandle = c.UrlHandle
                }).ToList()
            };
            if (!response.Equals(""))
            {
                message = $"Data Added successfully";
                success = true;

            }
            return new BlogPostResponse(success, message);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = IdentityDefaults.WriterRole)]
        public async Task<BlogPostSingleResponse> UpdateBlogPost([FromRoute] Guid id, UpdateBlogPostDTO updateBlogPostDTO)
        {
            bool success = false;
            string message = string.Empty;
            string data = string.Empty;
            // convert DTO to Doamin
            var blogPost = new BlogPost()
            {
                Id = id,
                Author = updateBlogPostDTO.Author,
                Content = updateBlogPostDTO.Content,
                FeaturedImageUrl = updateBlogPostDTO.FeaturedImageUrl,
                IsVisible = updateBlogPostDTO.IsVisible,
                PublishedDate = updateBlogPostDTO.PublishedDate,
                shortDescription = updateBlogPostDTO.shortDescription,
                Title = updateBlogPostDTO.Title,    
                UrlHandle = updateBlogPostDTO.UrlHandle,                                                        
                Categories = new List<Category>()
            };
            if (updateBlogPostDTO.categories != null)
            {
                foreach (var categoryId in updateBlogPostDTO.categories)
                {
                    var ExistCategory = await categorirsRepositories.GetById(categoryId);
                    if (ExistCategory != null)
                    {
                        blogPost.Categories.Add(ExistCategory);
                    }
                }
            }
            // call repositories
            var updatedBlogPost = await blogPostRepositories.updateBlogPost(blogPost);
            if (updatedBlogPost == null)
            {
                message = "Blof Post is not Updated Sucessafully !!";
                return new BlogPostSingleResponse(success, message, new BlogPostDTO());
            }

            // convert Domain to DTO
            var blogPostDTOData = new BlogPostDTO()
            {
                Id = id,
                Author = updatedBlogPost.Author,
                Content = updatedBlogPost.Content,
                FeaturedImageUrl = updatedBlogPost.FeaturedImageUrl,
                IsVisible = updatedBlogPost.IsVisible,
                PublishedDate = updatedBlogPost.PublishedDate,
                shortDescription = updatedBlogPost.shortDescription,
                Title = updatedBlogPost.Title,
                UrlHandle = updatedBlogPost.UrlHandle,
                categories = new List<CategoryDTO>()
            };

            blogPostDTOData.categories = updatedBlogPost.Categories?.Where(c => c != null)
                .Select(c => new CategoryDTO
                {
                    Name = c.Name,
                    Id = c.Id,
                    UrlHandle = c.UrlHandle,
                }).ToList() ?? new List<CategoryDTO>();

            //foreach (var categoryitem in blogPostDTOData.categories)
            //{
            //    blogPostDTOData.categories.Add(new CategoryDTO()
            //    {
            //        Id = categoryitem.Id,
            //        UrlHandle = categoryitem.UrlHandle,
            //        CategoryId = categoryitem.CategoryId,
            //        Name = categoryitem.Name,
            //    });
            //}

            success = true;
            message = "Blog Updated Succesfully !";
            return new BlogPostSingleResponse(success, message, blogPostDTOData);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = IdentityDefaults.WriterRole)]
        public async Task<BlogPostResponse> deleteBlogPost([FromRoute] Guid id)
        {
            bool success = false;
            string message = string.Empty;

            var softDeleteBlogPost = await blogPostRepositories.deleteBlogPost(id);
            if (!softDeleteBlogPost)
            {
                success = false;
                message = "Post not Found";

                new BlogPostResponse(success,message);
            }

            success = true;
            message = "Post deleted Succesfully !!";

            return new BlogPostResponse(success, message);
        }

    }

    public class FetchBlogPostResponse
    {
        public bool Success { get; init; }
        public string Message { get; init; }
        public IEnumerable<BlogPostDTO> Data { get; init; }
        public FetchBlogPostResponse(bool success, string message, List<BlogPostDTO> data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }

    public class FetchSingleBlogPostResponse
    {
        public bool Success { get; init; }
        public string Message { get; init; }
        public BlogPostDTO Data { get; init; }
        public FetchSingleBlogPostResponse(bool success, string message, BlogPostDTO data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }

    public class BlogPostResponse
    {
        public bool Success { get; init; }
        public string? Message { get; init; }

        public BlogPostResponse(bool success, string message)
        {
            this.Success = success;
            this.Message = message;
        }
    }

    public class BlogPostSingleResponse
    {
        public bool Success { get; init; }
        public string Message { get; init; }
        public BlogPostDTO? Data { get; init; }

        public BlogPostSingleResponse(bool success,string message, BlogPostDTO? data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}
