using MediatR;
using PostService.Model;
using PostService.Query;
using PostService.Repository;

namespace PostService.Handler
{
    public class GetPostByIdHandler : IRequestHandler<GetPostById, BlogPost>
    {
        private readonly IPostRepository _postRepository;

        public GetPostByIdHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<BlogPost> Handle(GetPostById query, CancellationToken cancellationToken) 
        {
            return await _postRepository.GetPostByIdWithComment(query.Id);
        }
    }
}
