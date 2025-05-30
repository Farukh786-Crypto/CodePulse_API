using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repositories.Interface
{
    public interface ICategorirsRepositories
    {
        Task<Category> CreateCategoryAsync(Category category);

        Task<IEnumerable<Category>> GetCategoriesAsync();

        Task<Category?> GetById(int id);

        Task<Category?> UpdateCategory(Category category);
        Task<bool> SoftDeleteCategory(int categoryId);
    }
}
