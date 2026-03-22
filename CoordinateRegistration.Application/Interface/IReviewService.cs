using CoordinateRegistration.Application.Dto;
using CoordinateRegistration.Application.Dto.Review;

namespace CoordinateRegistration.Application.Interface
{
    public interface IReviewService
    {
        Task<ServiceResult<ReviewDto>> AddReview(ReviewAddDto model);
        Task<ServiceResult<ReviewDto>> PutReview(ReviewPutDto model);
        Task<ServiceResult<ReviewDto>> GetReview(Guid hash);
        Task<ServiceResult<ReviewDto>> GetReviewByMakerPerson(Guid hashMarker);
        Task<ServiceResult<ReviewDto>> DeleteReview(Guid hashReview);
    }
}
