using CoordinateRegistration.Application.Dto.Comment;
using CoordinateRegistration.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoordinateRegistration.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CommentAddDto model)
        {
                var comment = await _commentService.AddComment(model);
                if (!comment.Success) return this.StatusCode(comment.StatusCode, comment);
                return this.StatusCode(comment.StatusCode, comment);

        }

        [HttpPut]
        public async Task<IActionResult> PutComment(CommentPutDto model)
        {
                var comment = await _commentService.PutComment(model);
                if (!comment.Success) return this.StatusCode(comment.StatusCode, comment);
                return this.StatusCode(comment.StatusCode, comment);

        }

        [HttpGet("{markerHash}")]
        public async Task<IActionResult> GetComments(Guid markerHash)
        {
                var comments = await _commentService.GetComments(markerHash);
                if (!comments.Success) return this.StatusCode(comments.StatusCode, comments);
                return this.StatusCode(comments.StatusCode, comments);

        }

        [HttpDelete("{commentHash}")]
        public async Task<IActionResult> DeleteComment(Guid commentHash)
        {
                var comment = await _commentService.DeleteComment(commentHash);
                if (!comment.Success) return this.StatusCode(comment.StatusCode, comment);
                return this.StatusCode(comment.StatusCode, comment);

        }
    }
}
