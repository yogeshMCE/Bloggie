using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.web.Models.ViewModels
{
    public class AddBlogPostRequest
    {
       
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublisedDate { get; set; }
        public string Auther { get; set; }

        public bool Visible { get; set; }

        // Display tags
        public IEnumerable<SelectListItem>Tags { get; set; }
        // Collect Tag
        public string[] SelectedTags { get; set; } = Array.Empty<string>();
    }
}
