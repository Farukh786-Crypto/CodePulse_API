using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementations
{
    public class BlogPostRepositories : IBlogPostRepositories
    {
        private readonly ApplicationDbContext applicationDbContext;

        public BlogPostRepositories(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task<BlogPost> blogPostAsync(BlogPost blogPost)
        {
            await applicationDbContext.BlogPosts.AddAsync(blogPost);
            applicationDbContext.SaveChanges();
            return blogPost;
        }

        public async Task<BlogPost?> getBlogPostByID(Guid Id)
        {
            return await applicationDbContext.BlogPosts.Include(i => i.Categories).FirstOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<BlogPost?> getBlogPostByUrlHandle(string urlHandle)
        {
            return await applicationDbContext.BlogPosts.Include(i => i.Categories).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<IEnumerable<BlogPost>> getAllBlogPost()
        {
            return await applicationDbContext.BlogPosts.Where(a=>a.IsVisible).Include(b=>b.Categories).ToListAsync();
        }

        public async Task<BlogPost?> updateBlogPost(BlogPost blogPost)
        {

            if (blogPost != null)
            {
                var exitBlogPost = await applicationDbContext.BlogPosts.Include(c => c.Categories).FirstOrDefaultAsync(x => x.Id == blogPost.Id);
                if (exitBlogPost is null)
                {
                    return null;
                }
                // Update BlogPost
                applicationDbContext.Entry(exitBlogPost).CurrentValues.SetValues(blogPost);
                //applicationDbContext.Update(blogPost);

                // update categories
                exitBlogPost.Categories = blogPost.Categories;
                await applicationDbContext.SaveChangesAsync();
                return blogPost;
            }
            return null;
        }

        public async Task<Boolean> deleteBlogPost(Guid id)
        {
            var exitBlog = await applicationDbContext.BlogPosts.FirstOrDefaultAsync(a => a.Id == id);
            if (exitBlog is null)
            {
                return false;
            }
            exitBlog.IsVisible = false;

            applicationDbContext.BlogPosts.Update(exitBlog);
            applicationDbContext.SaveChangesAsync();

            return true;

        }

    }
}
