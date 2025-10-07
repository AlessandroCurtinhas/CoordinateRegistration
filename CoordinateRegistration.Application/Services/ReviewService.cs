using AutoMapper;
using CoordinateRegistration.Application.Dto;
using CoordinateRegistration.Application.Dto.Review;
using CoordinateRegistration.Application.Interface;
using CoordinateRegistration.Application.Interface.Authenticate;
using CoordinateRegistration.Domain;
using CoordinateRegistration.Persistence.Interface;
using FluentValidation;

namespace CoordinateRegistration.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IValidator<ReviewAddDto> _reviewAddValidator;
        private readonly IValidator<ReviewPutDto> _reviewPutValidator;
        private readonly IAllRespository _allRepository;
        private readonly IUserAuthenticationService _userAuthenticatedService;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMarkerRepository _markerRepository;
        
        private readonly IMapper _mapper;

        public ReviewService(IValidator<ReviewAddDto> reviewAddValidator, IReviewRepository reviewRepository, IMarkerRepository markerRepository, IMapper mapper, IAllRespository allRepository, IUserAuthenticationService userAuthenticatedService, IValidator<ReviewPutDto> reviewPutValidator)
        {
            _reviewAddValidator = reviewAddValidator;
            _reviewRepository = reviewRepository;
            _markerRepository = markerRepository;
            _mapper = mapper;
            _allRepository = allRepository;
            _userAuthenticatedService = userAuthenticatedService;
            _reviewPutValidator = reviewPutValidator;
        }


        public async Task<ServiceResult<ReviewDto>> AddReview(ReviewAddDto model)
        {
            try
            {
                var userAuthenticaded = await _userAuthenticatedService.GetUserAuthenticated();
                if (userAuthenticaded == null || userAuthenticaded.Active == false) return ServiceResult<ReviewDto>.FailResult("Você não possui autorização para realizar esta operação");

                var resultValidation = _reviewAddValidator.Validate(model);
                if (!resultValidation.IsValid) return ServiceResult<ReviewDto>.FailResult(resultValidation);
      
                var marker = await _markerRepository.GetByHash(model.MarkerHash);
                if (marker == null) return ServiceResult<ReviewDto>.FailResult("O marcador não foi encontrado na base de dados.");

                var reviewValidate = await _reviewRepository.GetByUserMarker(userAuthenticaded.Id, marker.Id);
                if (reviewValidate != null) return ServiceResult<ReviewDto>.FailResult("Você já avaliou este marcador.");

                var review = _mapper.Map<Review>(model);
                review.UserId = userAuthenticaded.Id;
                review.MarkerId = marker.Id;
                review.UserId = userAuthenticaded.Id;

                _allRepository.Add(review);
                await _allRepository.SaveChangesAsync();

                var reviewReturn = await _reviewRepository.GetByHash(review.Hash);

                return ServiceResult<ReviewDto>.SucessResult(_mapper.Map<ReviewDto>(reviewReturn));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<ServiceResult<ReviewDto>> PutReview(ReviewPutDto model)
        {
            try
            {
                var userAuthenticaded = await _userAuthenticatedService.GetUserAuthenticated();
                if (userAuthenticaded == null || userAuthenticaded.Active == false) return ServiceResult<ReviewDto>.FailResult("Você não possui autorização para realizar esta operação");

                var resultValidation = _reviewPutValidator.Validate(model);
                if (!resultValidation.IsValid) return ServiceResult<ReviewDto>.FailResult(resultValidation);

                var review = await _reviewRepository.GetByHash(model.Hash);
                if (review == null) return ServiceResult<ReviewDto>.FailResult("A avaliação não foi encontrada na base de dados.");
                if (review.User.Id != userAuthenticaded.Id) return ServiceResult<ReviewDto>.FailResult("Você não possui autorização para realizar esta operação");

                _mapper.Map(model, review);

                _allRepository.Update(review);
                await _allRepository.SaveChangesAsync();

                var reviewReturn = await _reviewRepository.GetByHash(review.Hash);

                return ServiceResult<ReviewDto>.SucessResult(_mapper.Map<ReviewDto>(reviewReturn));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<ServiceResult<ReviewDto>> GetReview(Guid hashReview)
        {
            try
            {
                if (hashReview == Guid.Empty) return ServiceResult<ReviewDto>.FailResult("O hash não foi preenchido");

                var userAuthenticaded = await _userAuthenticatedService.GetUserAuthenticated();
                if (userAuthenticaded == null || userAuthenticaded.Active == false) return ServiceResult<ReviewDto>.FailResult("Você não possui autorização para realizar esta operação");

                if (hashReview == Guid.Empty) return ServiceResult<ReviewDto>.FailResult("O hash não foi preenchido");

                var review = await _reviewRepository.GetByHash(hashReview);
                if (review == null) return ServiceResult<ReviewDto>.FailResult("A avaliação não foi encontrado na base de dados.");

                return ServiceResult<ReviewDto>.SucessResult(_mapper.Map<ReviewDto>(review));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            

        }

        public async Task<ServiceResult<ReviewDto>> GetReviewByMakerUser(Guid hashMarker)
        {
            try
            {
                if (hashMarker == Guid.Empty) return ServiceResult<ReviewDto>.FailResult("O hash não foi preenchido");

                var userAuthenticaded = await _userAuthenticatedService.GetUserAuthenticated();
                if (userAuthenticaded == null || userAuthenticaded.Active == false) return ServiceResult<ReviewDto>.FailResult("Você não possui autorização para realizar esta operação");


                var review = await _reviewRepository.GetByUserMarker(userAuthenticaded.Id, hashMarker);
                if (review == null) return ServiceResult<ReviewDto>.FailResult("A avaliação não foi encontrada na base de dados.");

                return ServiceResult<ReviewDto>.SucessResult(_mapper.Map<ReviewDto>(review));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }

        public async Task<ServiceResult<ReviewDto>> DeleteReview(Guid hashReview)
        {
            try
            {
                if (hashReview == Guid.Empty) return ServiceResult<ReviewDto>.FailResult("O hash não foi preenchido");

                var userAuthenticaded = await _userAuthenticatedService.GetUserAuthenticated();
                if (userAuthenticaded == null || userAuthenticaded.Active == false) return ServiceResult<ReviewDto>.FailResult("Você não possui autorização para realizar esta operação");


                var review = await _reviewRepository.GetByHash(hashReview);
                if (review == null) return ServiceResult<ReviewDto>.FailResult("A avaliação não foi encontrada na base de dados.");

                if (review.User.Hash != userAuthenticaded.Hash) return ServiceResult<ReviewDto>.FailResult("Você não possui autorização para realizar esta operação");

                _allRepository.Delete(review);
                await _allRepository.SaveChangesAsync();

                return ServiceResult<ReviewDto>.SucessResult();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }
    }
}
