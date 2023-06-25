using Bloggie.web.Models.Domain;
using Bloggie.web.Models.ViewModels;
using Bloggie.web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Bloggie.web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminBlogPostController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IBlogPostRepository blogPostRepository;

        public AdminBlogPostController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            this.tagRepository = tagRepository;
            this.blogPostRepository = blogPostRepository;
        }
        [HttpGet]
        public  async Task<IActionResult> Add()
        {
            
            var tags = await tagRepository.GetAllAsync();

            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            // all values to the blogpost model.
            var blogpost = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                ShortDescription = addBlogPostRequest.ShortDescription,
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublisedDate = addBlogPostRequest.PublisedDate,
                Auther = addBlogPostRequest.Auther,
                Visible = addBlogPostRequest.Visible,
            };
            var taglist = new List<Tag>();
            foreach(var selectedtagid in addBlogPostRequest.SelectedTags)
            {
                var Guidtypeselectedtagid = Guid.Parse(selectedtagid);
                var Existingtag = await tagRepository.GetAsync(Guidtypeselectedtagid);
                if(Existingtag!=null)
                {
                    taglist.Add(Existingtag);
                }
            }
            blogpost.Tags= taglist;
            await blogPostRepository.AddAsync(blogpost);

            return RedirectToAction("List");
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var blogpost= await blogPostRepository.GetAllAsync();
            return View(blogpost);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var blogpost = await blogPostRepository.GetAsync(id);
            var tags = await tagRepository.GetAllAsync();
            if (blogpost != null)
            {
                var model = new EditBlogPost
                {
                    Heading = blogpost.Heading,
                    PageTitle = blogpost.PageTitle,
                    Content = blogpost.Content,
                    ShortDescription = blogpost.ShortDescription,
                    FeaturedImageUrl = blogpost.FeaturedImageUrl,
                    UrlHandle = blogpost.UrlHandle,
                    PublisedDate = blogpost.PublisedDate,
                    Auther = blogpost.Auther,
                    Visible = blogpost.Visible,
                    Tags = tags.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    SelectedTags = blogpost.Tags.Select(x => x.Id.ToString()).ToArray()
                };

                return View(model);
            }

            return View(null);
        
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPost editBlogPost)
        {
            var blogpost = new BlogPost
            {   ID = editBlogPost.ID,
                Heading = editBlogPost.Heading,
                PageTitle = editBlogPost.PageTitle,
                Content = editBlogPost.Content,
                ShortDescription = editBlogPost.ShortDescription,
                FeaturedImageUrl = editBlogPost.FeaturedImageUrl,
                UrlHandle = editBlogPost.UrlHandle,
                PublisedDate = editBlogPost.PublisedDate,
                Auther = editBlogPost.Auther,
                Visible = editBlogPost.Visible,

            };
            var taglist = new List<Tag>();
            foreach (var selectedTag in editBlogPost.SelectedTags)
            {
                if (Guid.TryParse(selectedTag, out var tag))
                {
                    var foundTag = await tagRepository.GetAsync(tag);

                    if (foundTag != null)
                    {
                        taglist.Add(foundTag);
                    }
                }
            }
            blogpost.Tags= taglist;
           var updatedBlogPost= await blogPostRepository.UpdateAsync(blogpost);
            if(updatedBlogPost!=null)
            {
                // show successfull notification
            }
            else
            {
                // show error message.
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditBlogPost editBlogPost)
        {
            var deletedBlog = await blogPostRepository.DeleteAsync(editBlogPost.ID);
            if(deletedBlog!=null) {
                // show successful notification
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Edit", new {id=editBlogPost.ID});
            }
        }
        
    }

}
