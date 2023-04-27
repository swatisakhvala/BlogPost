using CommentService.Model;

namespace CommentService.Repository
{
    public interface ICommentRepository
    {
        public Task<BlogComment> AddComment(BlogComment blogPost);
    }
}
