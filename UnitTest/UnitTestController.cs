using Moq;
using PostService.Model;
using PostService.Repository;
using PostService.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using CommentService.Model;
using CommentService.Repository;
using CommentService.Controllers;

namespace UnitTest
{
    public class UnitTestController
    {
        private readonly Mock<IPostRepository> PostRepository;
        private readonly Mock<ICommentRepository> CommentRepository;
        
        public UnitTestController()
        {
            PostRepository = new Mock<IPostRepository>();
            CommentRepository = new Mock<ICommentRepository>();
        }

        [Fact]
        public void GetBlogPostById_Post()
        {
            var mediator = new Mock<IMediator>();
            //arrange
            var postList = GetPostData();
            PostRepository.Setup(x => x.GetPostByIdWithComment(4).Result)
                .Returns(postList[1]);
            var postController = new PostController(mediator.Object);

            //act
            var postResult = postController.GetBlogPostByIdAsync(4);

            //assert
            Assert.NotNull(postResult);
        }

        [Fact]
        public void AddPost_Post()
        {
            var mediator = new Mock<IMediator>();
            //arrange
            var postList = GetPostData();
            PostRepository.Setup(x => x.AddPost(postList[1]).Result)
                .Returns(postList[1]);
            var postController = new PostController(mediator.Object);

            //act
            var postResult = postController.AddBlogPostAsync(postList[1]);

            //assert
            Assert.NotNull(postResult);
        }

        [Fact]
        public void AddComment_Comment()
        {
            var mediator = new Mock<IMediator>();
            //arrange
            var commentList = GetCommentData();
            CommentRepository.Setup(x => x.AddComment(commentList[1]).Result)
                .Returns(commentList[1]);
            var commentController = new CommentController(mediator.Object);

            //act
            var commentResult = commentController.AddCommentAsync(commentList[1]);

            //assert
            Assert.NotNull(commentResult);
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
                Id = 4,
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
