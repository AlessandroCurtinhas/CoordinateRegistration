using CoordinateRegistration.Application.Dto;
using CoordinateRegistration.Application.Dto.Comment;

namespace CoordinateRegistration.Application.Interface
{
    public interface ICommentService
    {
        Task<ServiceResult<CommentDto>> AddComment(CommentAddDto model);
        Task<ServiceResult<CommentDto>> PutComment(CommentPutDto model);
        Task<ServiceResult<IEnumerable<CommentDto>>> GetComments(Guid hashMarker);
        Task<ServiceResult<CommentDto>> DeleteComment(Guid hashMarker);
    }
}
