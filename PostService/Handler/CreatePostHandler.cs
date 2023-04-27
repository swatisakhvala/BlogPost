using MediatR;
using PostService.Command;
using PostService.Model;
using PostService.Query;
using PostService.Repository;

namespace PostService.Handler
{
    public class CreatePostHandler : IRequestHandler<CreatePostCommand, BlogPost>
    {
        private readonly IPostRepository _postRepository;

        public CreatePostHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<BlogPost> Handle(CreatePostCommand createPostCommand, CancellationToken cancellationToken) 
        {
            var post = new BlogPost()
            {
                Title = createPostCommand.Title,
                MetaTitle = createPostCommand.MetaTitle,
                Slag = createPostCommand.Slag,
                Summary = createPostCommand.Summary,
                PostContent = createPostCommand.PostContent,
                IsPublished = createPostCommand.IsPublished,
                PublishedOn = createPostCommand.PublishedOn,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                BlogComment = createPostCommand.BlogComment
            };

            return await _postRepository.AddPost(post);
        }
    }
}
