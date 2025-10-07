using CoordinateRegistration.Application.Dto.Marker;
using CoordinateRegistration.Application.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CoordinateRegistration.Application.Interface;
using CoordinateRegistration.Application.Dto.Review;

namespace CoordinateRegistration.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(ReviewAddDto model)
        {
            try
            {
                var review = await _reviewService.AddReview(model);
                if (!review.Success) return this.StatusCode(StatusCodes.Status422UnprocessableEntity, review);
                return this.StatusCode(StatusCodes.Status201Created, review);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<MarkerDto>.FailResult(ex.Message));
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutReview(ReviewPutDto model)
        {
            try
            {
                var review = await _reviewService.PutReview(model);
                if (!review.Success) return this.StatusCode(StatusCodes.Status422UnprocessableEntity, review);
                return this.StatusCode(StatusCodes.Status200OK, review);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<MarkerDto>.FailResult(ex.Message));
            }
        }

        [HttpGet("/Review/{hashReview}")]
        public async Task<IActionResult> GetReview(Guid hashReview)
        {
            try
            {
                var review = await _reviewService.GetReview(hashReview);
                if (!review.Success) return this.StatusCode(StatusCodes.Status422UnprocessableEntity, review);
                return this.StatusCode(StatusCodes.Status200OK, review);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<MarkerDto>.FailResult(ex.Message));
            }
        }

        [HttpGet("/markerReview/{hashMaker}")]
        public async Task<IActionResult> GetReviewByMarker(Guid hashMaker)
        {
            try
            {
                var review = await _reviewService.GetReviewByMakerUser(hashMaker);
                if (!review.Success) return this.StatusCode(StatusCodes.Status422UnprocessableEntity, review);
                return this.StatusCode(StatusCodes.Status200OK, review);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<MarkerDto>.FailResult(ex.Message));
            }
        }

        [HttpDelete("/{hashReview}")]
        public async Task<IActionResult> DeleteReview(Guid hashReview)
        {
            try
            {
                var review = await _reviewService.DeleteReview(hashReview);
                if (!review.Success) return this.StatusCode(StatusCodes.Status422UnprocessableEntity, review);
                return this.StatusCode(StatusCodes.Status200OK, review);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<MarkerDto>.FailResult(ex.Message));
            }
        }
    }
}
