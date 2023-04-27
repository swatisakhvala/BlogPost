using CommentService.Model;
using MediatR;

namespace CommentService.Command
{
    public class CreateCommentCommand :IRequest<BlogComment>
    {
        public int BlogPostId { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public bool IsPublished { get; set; }
        public DateTime PublishedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public CreateCommentCommand(int BlogPostId, string Title, string Comment, bool IsPublished, DateTime PublishedOn, DateTime CreatedOn, DateTime UpdatedOn) 
        {
            this.Title = Title;
            this.BlogPostId = BlogPostId;
            this.Comment = Comment;
            this.IsPublished = IsPublished;
            this.PublishedOn = PublishedOn;
            this.CreatedOn = CreatedOn;
            this.UpdatedOn = UpdatedOn;
        }
    }
}
