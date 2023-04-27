using PostService.Model;

namespace PostService.Repository
{
    public interface IPostRepository
    {
        public Task<BlogPost> AddPost(BlogPost blogPost);

        public Task<BlogPost> GetPostByIdWithComment(int Id);
    }
}
