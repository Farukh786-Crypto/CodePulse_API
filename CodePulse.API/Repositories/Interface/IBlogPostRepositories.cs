using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repositories.Interface
{
    public interface IBlogPostRepositories
    {
        Task<BlogPost> blogPostAsync(BlogPost blogPost);

        Task<BlogPost?> getBlogPostByID(Guid Id);

        Task<BlogPost?> getBlogPostByUrlHandle(string urlHandle);

        Task<IEnumerable<BlogPost>> getAllBlogPost();

        Task<BlogPost?> updateBlogPost(BlogPost blogPost);

        Task<Boolean> deleteBlogPost(Guid id);
    }
}
