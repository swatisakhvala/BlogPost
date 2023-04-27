using CommentService.Command;
using CommentService.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace CommentService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IMediator mediator;
        public CommentController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<dynamic> AddCommentAsync(BlogComment blogComment)
        {
            var CommentDetails = await mediator.Send(new CreateCommentCommand(
                blogComment.BlogPostId,
                blogComment.Title,
                blogComment.Comment,
                blogComment.IsPublished,
                blogComment.PublishedOn,
                blogComment.CreatedOn,
                blogComment.UpdatedOn
                ));

            return CommentDetails;
        }
    }
}
