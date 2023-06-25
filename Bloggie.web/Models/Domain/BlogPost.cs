namespace Bloggie.web.Models.Domain
{
    public class BlogPost
    {
        public Guid ID { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublisedDate { get; set; }
        public string Auther { get; set; }

        public bool Visible { get; set; }

        public ICollection<Tag>Tags { get; set; }
        public ICollection<BlogPostLike>Likes { get; set; }
    }
}
