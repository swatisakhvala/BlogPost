using MediatR;
using PostService.Model;

namespace PostService.Query
{
    public class GetPostById : IRequest<BlogPost>
    {
        public int Id { get; set; }
    }
}
