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
                var review = await _reviewService.AddReview(model);
                if (!review.Success) return this.StatusCode(review.StatusCode, review);
                return this.StatusCode(review.StatusCode, review);

        }

        [HttpPut]
        public async Task<IActionResult> PutReview(ReviewPutDto model)
        {
                var review = await _reviewService.PutReview(model);
                if (!review.Success) return this.StatusCode(review.StatusCode, review);
                return this.StatusCode(review.StatusCode, review);

        }

        [HttpGet("/Review/{hashReview}")]
        public async Task<IActionResult> GetReview(Guid hashReview)
        {
                var review = await _reviewService.GetReview(hashReview);
                if (!review.Success) return this.StatusCode(review.StatusCode, review);
                return this.StatusCode(review.StatusCode, review);

        }

        [HttpGet("/markerReview/{hashMaker}")]
        public async Task<IActionResult> GetReviewByMarker(Guid hashMaker)
        {
                var review = await _reviewService.GetReviewByMakerPerson(hashMaker);
                if (!review.Success) return this.StatusCode(review.StatusCode, review);
                return this.StatusCode(review.StatusCode, review);

        }

        [HttpDelete("/{hashReview}")]
        public async Task<IActionResult> DeleteReview(Guid hashReview)
        {
                var review = await _reviewService.DeleteReview(hashReview);
                if (!review.Success) return this.StatusCode(review.StatusCode, review);
                return this.StatusCode(review.StatusCode, review);

        }
    }
}
