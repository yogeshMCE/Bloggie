using Bloggie.web.Data;
using Bloggie.web.Models.Domain;
using Bloggie.web.Models.ViewModels;
using Bloggie.web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminTagsController : Controller
    {
        
        private readonly ITagRepository tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        [HttpGet]
        [ActionName("Add")]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };
            await tagRepository.AddAsync(tag);
            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public  async Task<IActionResult> List()
        {
            var tags = await tagRepository.GetAllAsync();
            return View(tags);
        }
        [HttpGet]

        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await tagRepository.GetAsync(id);
            if(tag !=null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName,

                };
                return View(editTagRequest);
            }
            return View(null);
        }

        [HttpPost]
        public   async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };
            var updatedTag =  await tagRepository.UpdateAsync(tag);
            if(updatedTag != null)
            {
                // show successfull notification.
            }
            else
            {
                // show error massage.
            }
            return RedirectToAction("List");

        }
        [HttpPost]
        [ActionName("Delete")]
        public  async Task<IActionResult> Delete(EditTagRequest request)
        {
            var tag= await tagRepository.DeleteAsync(request.Id);
            if(tag != null)
            {
                // show succsesfull notification
            }
            else
            {
                // show error message
                return RedirectToAction("edit", new { id = request.Id });
            }


            return RedirectToAction("List");
        }
     
    }
}
