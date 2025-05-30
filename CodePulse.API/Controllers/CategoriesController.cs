using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategorirsRepositories categorirsRepositories;

        public CategoriesController(ICategorirsRepositories categorirsRepositories)
        {
            this.categorirsRepositories = categorirsRepositories;
        }

        [HttpGet]
        public async Task<FetchCategoryResponse> GetCategories()
        {
            string message = string.Empty;
            bool success = false;
            string data = string.Empty;
            var categories = await categorirsRepositories.GetCategoriesAsync();
            // map domain model to DTO
            var responseData = new List<CategoryDTO>();
            foreach (var category in categories)
            {
                responseData.Add(new CategoryDTO
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle
                });
                success = true;
                message = $"Data fetched successfully.";
            }
            return new FetchCategoryResponse(success, message, responseData);
        }

        [HttpGet]
        [Route("{categoryId:int}")]

        public async Task<FetchCategorySingleResponse> GetCategoryByID([FromRoute] int categoryId)
        {
            string message = string.Empty;
            bool success = false;
            string data = string.Empty;

            var isExistCategory = await categorirsRepositories.GetById(categoryId);
            if (isExistCategory is null)
            {
                message = "Category not found.";
                return new FetchCategorySingleResponse(success, message, new CategoryDTO());
            }

            var responseData = new CategoryDTO
            {
                Id = isExistCategory.Id,
                Name = isExistCategory.Name,
                UrlHandle = isExistCategory.UrlHandle
            };
            success = true;
            message = "Category Fetch Successfully !!!";

            return new FetchCategorySingleResponse(success, message, responseData);
        }


        [HttpPost]
        [Authorize(Roles = IdentityDefaults.WriterRole)]
        public async Task<CategoryResponse> CreateCategory([FromBody] CreateCategoriesRequestDTO request)
        {
            string message = String.Empty;
            bool success = false;
            // map DTO to Domain Model
            Category category = new Category()
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            await this.categorirsRepositories.CreateCategoryAsync(category);

            // Domain model to DTO
            var response = new CategoryDTO()
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };
            if (!response.Equals(""))
            {
                message = $"Data Added successfully";
                success = true;
            }
            return new CategoryResponse(success, message);
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = IdentityDefaults.WriterRole)]
        public async Task<FetchCategorySingleResponse> EditCategory([FromRoute] int id, UpdateCategoryDTO updateCategoryDTO)
        {
            string message = string.Empty;
            bool success = false;
            string data = string.Empty;
            // convert DTO to domain
            var category = new Category
            {
                Id = id,
                Name = updateCategoryDTO.Name,
                UrlHandle = updateCategoryDTO.UrlHandle
            };

            category = await this.categorirsRepositories.UpdateCategory(category);
            if (category == null)
            {
                message = "Category not found.";
                return new FetchCategorySingleResponse(success, message, new CategoryDTO());
            }

            // convert Domain to DTO
            var respons = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };
            success = true;
            message = "Category Edit Successfully !!!";

            return new FetchCategorySingleResponse(success, message, respons);
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = IdentityDefaults.WriterRole)]
        public async Task<CategoryResponse> DeleteCategory([FromRoute]int id)
        {
            string message = string.Empty;
            bool success = false;
            string data = string.Empty;
            var successData = await this.categorirsRepositories.SoftDeleteCategory(id);
            if (successData)
            {
                success = true;
                message = "Category Deleted Successfully !!";
                return new CategoryResponse(success, message);
            }
            success = false;
            message = "Category not found or already deleted";
            return new CategoryResponse(success,message);
        }
    }

    public class FetchCategorySingleResponse
    {
        public bool Success { get; init; }
        public string Message { get; init; }
        public CategoryDTO? Data { get; init; } 

        public FetchCategorySingleResponse(bool success, string message, CategoryDTO? data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }


    public class FetchCategoryResponse
    {
        public bool Success { get; init; }
        public string Message { get; init; }
        public IEnumerable<CategoryDTO> Data { get; init; }
        public FetchCategoryResponse(bool success, string message, List<CategoryDTO> data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }

    public class CategoryResponse
    {
        public bool Success { get; init; }
        public string? Message { get; init; }

        public CategoryResponse(bool success, string message)
        {
            this.Success = success;
            this.Message = message;
        }
    }
}
