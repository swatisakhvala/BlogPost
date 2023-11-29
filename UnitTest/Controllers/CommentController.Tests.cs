using CommentService.Command;
using CommentService.Handler;
using CommentService.Model;
using Microsoft.Extensions.Configuration;
using CommentService.Repository;
using CommentService.Data;


namespace UnitTest.Controllers
{
    public class CommentController
    {
        private IConfiguration _config;
        public CommentController()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            _config = builder.Build();

        }

        [Fact]
        public async Task AddComment_Comment()
        {
            //arrange
            var commentList = GetCommentData().LastOrDefault();


            using var dbContext = new DbContextClass(_config);
            var CommentRepository = new CommentRepository(dbContext);
            var handler = new CreateCommentHandler(CommentRepository);
            var query = new CreateCommentCommand(commentList.BlogPostId, commentList.Title, commentList.Comment, commentList.IsPublished, commentList.PublishedOn, DateTime.Now, DateTime.Now);

            //act
            var commentResult = await handler.Handle(query, CancellationToken.None);

            //assert
            Assert.NotNull(commentResult);
            Assert.NotEqual(0, commentResult.Id);
            Assert.Contains(commentResult.Comment, GetCommentData().Select(x => x.Comment));
        }

        [Fact]
        public async Task AddComment_NullCommand_ThrowsArgumentNullException()
        {
            using var dbContext = new DbContextClass(_config);
            var CommentRepository = new CommentRepository(dbContext);
            var handler = new CreateCommentHandler(CommentRepository);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await handler.Handle(null, CancellationToken.None));
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
