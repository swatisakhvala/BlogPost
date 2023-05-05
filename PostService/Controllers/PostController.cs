using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using PostService.Command;
using PostService.Model;
using PostService.Query;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace PostService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IDistributedCache _cache;
        public PostController(IMediator mediator, IDistributedCache cache)
        {
            this.mediator = mediator;
            _cache = cache;
        }

        [HttpGet]
        [Route("GetBlogPostByIdAsync/{Id}/{enableCache}")]
        public async Task<dynamic> GetBlogPostByIdAsync(int Id, bool enableCache)
        {
            if (!enableCache)
            {
                var PostDetails = await mediator.Send(new GetPostById() { Id = Id });
                return PostDetails;
            }

            string cacheKey = Id.ToString();

            // Trying to get data from the Redis cache
            byte[] cachedData = await _cache.GetAsync(cacheKey);

            BlogPost blogPost = new();
            if (cachedData != null)
            {
                // If the data is found in the cache, encode and deserialize cached data.
                var cachedDataString = Encoding.UTF8.GetString(cachedData);
                blogPost = JsonSerializer.Deserialize<BlogPost>(cachedDataString);
            }
            else
            {
                // If the data is not found in the cache, then fetch data from database
                blogPost = await mediator.Send(new GetPostById() { Id = Id });

                // Serializing the data
                string cachedDataString = JsonSerializer.Serialize(blogPost);
                var dataToCache = Encoding.UTF8.GetBytes(cachedDataString);

                // Setting up the cache options
                DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(5))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(3));

                // Add the data into the cache
                await _cache.SetAsync(cacheKey, dataToCache, options);
            }

            return blogPost;
        }

        [HttpPost]
        public async Task<dynamic> AddBlogPostAsync(BlogPost blogPost)
        {
            var PostDetails = await mediator.Send(new CreatePostCommand(
                blogPost.Title,
                blogPost.MetaTitle,
                blogPost.Slag,
                blogPost.Summary,
                blogPost.PostContent,
                blogPost.IsPublished,
                blogPost.PublishedOn,
                blogPost.CreatedOn,
                blogPost.UpdatedOn,
                blogPost.BlogComment
                ));

            return PostDetails;
        }
    }
}
