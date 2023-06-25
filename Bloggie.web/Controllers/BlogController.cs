using Bloggie.web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;

        public BlogController(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }
        public  async Task<IActionResult> Index(string UrlHandle)
         {
            var blogpost = await  blogPostRepository.GetByUrlHandleAsync(UrlHandle);
            return View(blogpost);
        }
    }
}
