using MediatR;
using PostService.Model;

namespace PostService.Command
{
    public class CreatePostCommand :IRequest<BlogPost>
    {
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
        public CreatePostCommand(string Title, string MetaTitle, string Slag, string Summary, string PostContent, bool IsPublished, DateTime PublishedOn, DateTime CreatedOn, DateTime UpdatedOn, List<BlogPostComment> BlogComment) 
        {
            this.Title = Title;
            this.MetaTitle = MetaTitle;
            this.Slag = Slag;
            this.Summary = Summary;
            this.PostContent = PostContent;
            this.IsPublished = IsPublished;
            this.PublishedOn = PublishedOn;
            this.CreatedOn = CreatedOn;
            this.UpdatedOn = UpdatedOn;
            this.BlogComment = BlogComment;
        }
    }
}
