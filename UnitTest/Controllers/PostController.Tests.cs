using Microsoft.Extensions.Configuration;
using PostService.Command;
using PostService.Handler;
using PostService.Model;
using PostService.Query;
using PostService.Repository;
using PostService.Data;

namespace UnitTest.Controllers
{
    public class PostController
    {
        private IConfiguration _config;
        public PostController()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            _config = builder.Build();

        }

        [Fact]
        public async Task GetBlogPostById_Post()
        {
            //arrange
            var postList = GetPostData();

            using var dbContext = new DbContextClass(_config);
            var PostRepository = new PostRepository(dbContext);
            var handler = new GetPostByIdHandler(PostRepository);
            var query = new GetPostById { Id = 2 };

            int invalidId = 100;

            //act
            var postResult = await handler.Handle(query, CancellationToken.None);
            var result = await handler.Handle(new GetPostById { Id = invalidId }, CancellationToken.None);

            //assert
            Assert.Equal(postList[1].Id, postResult.Id);
            Assert.NotNull(postResult);

            Assert.Null(result); // Ensure null is returned for an invalid ID
        }

        [Fact]
        public async Task AddPost_Post()
        {
            //arrange
            var postList = GetPostData().LastOrDefault();

            using var dbContext = new DbContextClass(_config);
            var PostRepository = new PostRepository(dbContext);
            var handler = new CreatePostHandler(PostRepository);
            var query = new CreatePostCommand(postList.Title, postList.MetaTitle, postList.Slag, postList.Summary, postList.PostContent, false, DateTime.Now, DateTime.Now, DateTime.Now, null);

            //act
            var postResult = await handler.Handle(query, CancellationToken.None);

            //assert
            Assert.NotNull(postResult);
            Assert.NotEqual(0, postResult.Id);
            Assert.Contains(postResult.Title, GetPostData().Select(x => x.Title));
        }

        [Fact]
        public async Task AddPost_NullCommand_ThrowsArgumentNullException()
        {
            using var dbContext = new DbContextClass(_config);
            var PostRepository = new PostRepository(dbContext);
            var handler = new CreatePostHandler(PostRepository);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await handler.Handle(null, CancellationToken.None));
        }



        private List<BlogPost> GetPostData()
        {
            List<BlogPost> postData = new List<BlogPost>
        {
            new BlogPost
            {
                Id = 1,
                Title = "TechCrunch",
                MetaTitle = "TechCrunch",
                Slag = "Technology",
                Summary = "The blog’s target audience is technology and business enthusiasts, especially startup founders and investors worldwide",
                PostContent = "blog that provides technology and startup news, from the latest developments in Silicon Valley to venture capital funding",
                IsPublished = true,
                PublishedOn= DateTime.Now,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                BlogComment = null
            },
             new BlogPost
            {
                Id = 2,
                Title = "Engadget",
                MetaTitle = "Engadget",
                Slag = "Technology",
                Summary = "Technology blog providing reviews of gadgets and consumer electronics as well as the latest news in the tech world",
                PostContent = "The blog’s simple black-and-white theme gives it a sleek look fitting for a technology blog",
                IsPublished = true,
                PublishedOn= DateTime.Now,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                BlogComment = null
            },
        };
            return postData;
        }


    }
}
