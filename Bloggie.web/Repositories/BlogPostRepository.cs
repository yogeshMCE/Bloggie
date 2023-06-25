using Bloggie.web.Data;
using Bloggie.web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public BlogPostRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }
        public async Task<BlogPost> AddAsync(BlogPost post)
        {
            await bloggieDbContext.BlogPosts.AddAsync(post);
            await bloggieDbContext.SaveChangesAsync();
            return post; 
        }

        async Task<BlogPost> IBlogPostRepository.DeleteAsync(Guid id)
        {
            var deletedblog = await bloggieDbContext.BlogPosts.FindAsync(id);
            if(deletedblog != null)
            {
                bloggieDbContext.BlogPosts.Remove(deletedblog);
               await bloggieDbContext.SaveChangesAsync();
                return deletedblog;
            }
            return null;
        }

       public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await bloggieDbContext.BlogPosts.Include(x=>x.Tags).ToListAsync();
        }

         public async Task<BlogPost>GetAsync(Guid id)
        {
            return await bloggieDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<BlogPost?>UpdateAsync(BlogPost post)
        {
            var existingblogpost=  await bloggieDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.ID == post.ID);
            if (existingblogpost != null)
            {
                existingblogpost.ID = post.ID;
                existingblogpost.Heading = post.Heading;
                existingblogpost.PageTitle= post.PageTitle;
                existingblogpost.Content = post.Content;
                existingblogpost.ShortDescription= post.ShortDescription;
                existingblogpost.FeaturedImageUrl= post.FeaturedImageUrl;
                existingblogpost.UrlHandle= post.UrlHandle;
                existingblogpost.PublisedDate= post.PublisedDate;
                existingblogpost.Auther= post.Auther;
                existingblogpost.Visible= post.Visible;
                existingblogpost.Tags= post.Tags;
                await bloggieDbContext.SaveChangesAsync();
                return existingblogpost;

            }
            return null;

            
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            var blogpost= await bloggieDbContext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x=> x.UrlHandle==urlHandle);
            return blogpost;
        }
    }
}
