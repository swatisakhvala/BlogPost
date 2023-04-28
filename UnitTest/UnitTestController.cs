using Moq;
using PostService.Model;
using PostService.Repository;
using CommentService.Model;
using CommentService.Repository;
using PostService.Handler;
using PostService.Query;
using PostService.Data;
using Microsoft.Extensions.Configuration;
using PostService.Command;
using CommentService.Handler;
using CommentService.Command;

namespace UnitTest
{
    public class UnitTestController
    {
        private IConfiguration _config;
        public UnitTestController()
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

            //act
            var postResult = await handler.Handle(query, CancellationToken.None);

            //assert
            Assert.Equal(postList[1].Id, postResult.Id);
            Assert.NotNull(postResult);
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
        }

        [Fact]
        public async Task AddComment_Comment()
        {
            //arrange
            var commentList = GetCommentData().LastOrDefault();


            using var dbContext = new CommentService.Data.DbContextClass(_config);
            var CommentRepository = new CommentRepository(dbContext);
            var handler = new CreateCommentHandler(CommentRepository);
            var query = new CreateCommentCommand(commentList.BlogPostId, commentList.Title, commentList.Comment, commentList.IsPublished, commentList.PublishedOn, DateTime.Now, DateTime.Now);

            //act
            var commentResult = await handler.Handle(query, CancellationToken.None);

            //assert
            Assert.NotNull(commentResult);
            Assert.NotEqual(0, commentResult.Id);
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


        private List<BlogComment> GetCommentData()
        {
            List<BlogComment> commentData = new List<BlogComment>
        {
            new BlogComment
            {
                Id = 1,
                BlogPostId = 1,
                Title = "TechCrunch",
                Comment = "Great share!",
                IsPublished = true,
                PublishedOn= DateTime.Now,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
            },
             new BlogComment
            {
                Id = 2,
                BlogPostId = 1,
                Title = "Engadget",
                Comment = "Amazing write-up!",
                IsPublished = true,
                PublishedOn= DateTime.Now,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
            },
        };
            return commentData;
        }
    }
}
