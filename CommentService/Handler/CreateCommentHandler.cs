using CommentService.Model;
using CommentService.Repository;
using MediatR;
using CommentService.Command;

namespace CommentService.Handler
{
    public class CreateCommentHandler : IRequestHandler<CreateCommentCommand, BlogComment>
    {
        private readonly ICommentRepository _commentRepository;

        public CreateCommentHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<BlogComment> Handle(CreateCommentCommand createPostCommand, CancellationToken cancellationToken) 
        {
            var comment = new BlogComment()
            {
                Title = createPostCommand.Title,
                BlogPostId= createPostCommand.BlogPostId,
                Comment= createPostCommand.Comment,
                IsPublished = createPostCommand.IsPublished,
                PublishedOn = createPostCommand.PublishedOn,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };

            return await _commentRepository.AddComment(comment);
        }
    }
}
