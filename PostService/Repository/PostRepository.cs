using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using PostService.Data;
using PostService.Model;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace PostService.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly DbContextClass _dbContext;
        public PostRepository(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BlogPost> AddPost(BlogPost blogPost)
        {
            var result = _dbContext.BlogPost.Add(blogPost);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<BlogPost> GetPostByIdWithComment(int Id) 
        {
            try
            {
                var postlist = await _dbContext.BlogPost.Include(x => x.BlogComment).Where(x => x.Id == Id).FirstOrDefaultAsync();
                return postlist;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
    }
}
