using Bloggie.web.Models.Domain;

namespace Bloggie.web.Repositories
{
    public interface IBlogPostRepository
    {
        public Task<IEnumerable<BlogPost>> GetAllAsync();
        public Task<BlogPost> GetAsync(Guid id);
        public Task<BlogPost> AddAsync(BlogPost post);
        public Task<BlogPost?> UpdateAsync(BlogPost post);
        public Task<BlogPost?> GetByUrlHandleAsync(string urlHandle);
        public Task<BlogPost> DeleteAsync(Guid id);




    }
}
