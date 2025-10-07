using CoordinateRegistration.Application.Dto;
using CoordinateRegistration.Application.Dto.Comment;
using CoordinateRegistration.Application.Dto.Marker;
using CoordinateRegistration.Application.Interface;
using CoordinateRegistration.Application.Services;
using CoordinateRegistration.Domain;
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
            try
            {
                var comment = await _commentService.AddComment(model);
                if (!comment.Success) return this.StatusCode(StatusCodes.Status422UnprocessableEntity, comment);
                return this.StatusCode(StatusCodes.Status201Created, comment);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<MarkerDto>.FailResult(ex.Message));
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutComment(CommentPutDto model)
        {
            try
            {
                var comment = await _commentService.PutComment(model);
                if (!comment.Success) return this.StatusCode(StatusCodes.Status422UnprocessableEntity, comment);
                return this.StatusCode(StatusCodes.Status200OK, comment);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<MarkerDto>.FailResult(ex.Message));
            }
        }

        [HttpGet("{markerHash}")]
        public async Task<IActionResult> GetComments(Guid markerHash)
        {
            try
            {
                var comments = await _commentService.GetComments(markerHash);
                if (!comments.Success) return this.StatusCode(StatusCodes.Status422UnprocessableEntity, comments);
                if (comments.Success && comments.Data.Count() == 0) return this.StatusCode(StatusCodes.Status204NoContent, comments);
                return this.StatusCode(StatusCodes.Status200OK, comments);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<MarkerDto>.FailResult(ex.Message));
            }
        }

        [HttpDelete("{commentHash}")]
        public async Task<IActionResult> DeleteComment(Guid commentHash)
        {
            try
            {
                var comment = await _commentService.DeleteComment(commentHash);
                if (!comment.Success) return this.StatusCode(StatusCodes.Status422UnprocessableEntity, comment);
                return this.StatusCode(StatusCodes.Status200OK, comment);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<MarkerDto>.FailResult(ex.Message));
            }
        }
    }
}
