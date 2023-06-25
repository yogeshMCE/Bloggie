using Bloggie.web.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bloggie.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {    private readonly IimageRespository iimageRespository;
        public ImagesController(IimageRespository iimageRespository)
        {
            this.iimageRespository = iimageRespository;
        }

        [HttpPost]
        public async Task<IActionResult> UplaodAsync(IFormFile file)
        {
            var imageurl = await iimageRespository.UploadAsync(file);
            if(imageurl == null)
            {
                return Problem("Something went wrong ", null, (int)HttpStatusCode.InternalServerError);

            }
            else
            {
                return new JsonResult(new { link = imageurl });
            }
        }
    }
}
