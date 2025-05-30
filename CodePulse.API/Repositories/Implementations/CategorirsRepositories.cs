using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementations
{
    public class CategorirsRepositories : ICategorirsRepositories
    {
        private readonly ApplicationDbContext applicationDbContext;
        public CategorirsRepositories(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task<Category> CreateCategoryAsync(Category category)
        {
            await applicationDbContext.Categories.AddAsync(category);
            // save this changes to database
            await applicationDbContext.SaveChangesAsync();
            return category;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await applicationDbContext.Categories.Where(c => !c.IsDeleted).ToListAsync();
        }

        async Task<Category?> ICategorirsRepositories.GetById(int id)
        {
            return await applicationDbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category?> UpdateCategory(Category category)
        {
            var existingCategory = await applicationDbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
            if (existingCategory != null)
            {
                applicationDbContext.Entry(existingCategory).CurrentValues.SetValues(category);
                await applicationDbContext.SaveChangesAsync();
                return existingCategory;
            }
            return null;
        }

        public async Task<bool> SoftDeleteCategory(int categoryId)
        {
            var existingCategory = await applicationDbContext.Categories.FirstAsync(x => x.Id == categoryId);
            if (existingCategory is null || existingCategory.IsDeleted)
            {
                return false;
            }
            existingCategory.IsDeleted = true;
            existingCategory.DeletedAt = DateTime.UtcNow;

            applicationDbContext.Update(existingCategory);
            await applicationDbContext.SaveChangesAsync();
            return true;
        }
    }
}
