using Azure.Core;
using Bloggie.web.Data;
using Bloggie.web.Models.Domain;
using Bloggie.web.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Repositories
{
    public class TagRepository : ITagRepository

    {
        private readonly BloggieDbContext bloggieDbContext;

        public TagRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {
            await bloggieDbContext.Tags.AddAsync(tag);
            await bloggieDbContext.SaveChangesAsync();
            return tag;
        }

         public async Task<Tag?>DeleteAsync(Guid id)
        {
            var tag = await GetAsync(id);
            if (tag != null)
            {
                bloggieDbContext.Tags.Remove(tag);
                await bloggieDbContext.SaveChangesAsync();
                return tag;
            }
            return null;
        }

         public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            var tags= await bloggieDbContext.Tags.ToListAsync();
            return tags;
        }

         public async Task<Tag>GetAsync(Guid id)
        {
            var tag =  await bloggieDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
            return tag;
        }

        public async Task<Tag?>UpdateAsync(Tag tag)
        {
            var existingtag = await GetAsync(tag.Id);
            if (existingtag != null)
            {
                existingtag.Name = tag.Name;
                existingtag.DisplayName = tag.DisplayName;
                await bloggieDbContext.SaveChangesAsync();
                return existingtag;
            }
            return null;
        }
    }
}
