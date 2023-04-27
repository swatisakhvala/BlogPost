
namespace PostService.Model
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string Slag { get; set; }
        public string Summary { get; set; }
        public string PostContent { get; set; }
        public bool IsPublished { get; set; }
        public DateTime PublishedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public List<BlogPostComment> BlogComment { get; set; }

    }
}
