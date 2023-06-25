using Bloggie.web.Models.Domain;

namespace Bloggie.web.Repositories
{
    public interface ITagRepository
    {
        // will define all methods for manupulate with the Tag table in BlogDB Using BloggieDbContext.
        //1 to add one tag in table
       public Task<Tag> AddAsync(Tag tag);

        //2 to delete one tag in table
       public Task<Tag?> DeleteAsync(Guid id);
       
        // 3 to fetch all tags from table
        public Task<IEnumerable<Tag>> GetAllAsync();

        // 4 to fetch  one tag from table;
        public Task<Tag> GetAsync(Guid id);

        // 5 to update a tag
        public Task<Tag?> UpdateAsync(Tag tag);


    }
}
