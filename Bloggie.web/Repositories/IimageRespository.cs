namespace Bloggie.web.Repositories
{
    public interface IimageRespository
    {
        public  Task<string> UploadAsync(IFormFile file);
    }
}
