using CommentService.Data;
using CommentService.Model;

namespace CommentService.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DbContextClass _dbContext;
        public CommentRepository(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BlogComment> AddComment(BlogComment blogComment)
        {
            var result = _dbContext.BlogComment.Add(blogComment);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}
