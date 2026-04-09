using AutoMapper;
using CoordinateRegistration.Application.Dto;
using CoordinateRegistration.Application.Dto.Marker;
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
        private readonly IPersonAuthenticationService _personAuthenticatedService;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMarkerRepository _markerRepository;
        
        private readonly IMapper _mapper;

        public ReviewService(IValidator<ReviewAddDto> reviewAddValidator, IReviewRepository reviewRepository, IMarkerRepository markerRepository, IMapper mapper, IAllRespository allRepository, IPersonAuthenticationService personAuthenticatedService, IValidator<ReviewPutDto> reviewPutValidator)
        {
            _reviewAddValidator = reviewAddValidator;
            _reviewRepository = reviewRepository;
            _markerRepository = markerRepository;
            _mapper = mapper;
            _allRepository = allRepository;
            _personAuthenticatedService = personAuthenticatedService;
            _reviewPutValidator = reviewPutValidator;
        }


        public async Task<ServiceResult<ReviewDto>> AddReview(ReviewAddDto model)
        {
            try
            {
                if (model == null) return ServiceResult<ReviewDto>.FailResult("O Json está mal formatado.", 400);

                var personAuthenticaded = await _personAuthenticatedService.GetPersonAuthenticated();
                if (personAuthenticaded == null || personAuthenticaded.Active == false) return ServiceResult<ReviewDto>.FailResult("Você não possui autorização para realizar esta operação", 401);

                var resultValidation = _reviewAddValidator.Validate(model);
                if (!resultValidation.IsValid) return ServiceResult<ReviewDto>.FailResult(resultValidation, 400);
      
                var marker = await _markerRepository.GetByHash(model.MarkerHash);
                if (marker == null) return ServiceResult<ReviewDto>.FailResult("O marcador não foi encontrado na base de dados.", 404);

                var reviewValidate = await _reviewRepository.GetByPersonMarker(personAuthenticaded.Id, marker.Id);
                if (reviewValidate != null) return ServiceResult<ReviewDto>.FailResult("Você já avaliou este marcador.", 400);

                var review = _mapper.Map<Review>(model);
                review.PersonId = personAuthenticaded.Id;
                review.MarkerId = marker.Id;
                review.PersonId = personAuthenticaded.Id;

                _allRepository.Add(review);
                await _allRepository.SaveChangesAsync();

                var reviewReturn = await _reviewRepository.GetByHash(review.Hash);

                return ServiceResult<ReviewDto>.SucessResult(_mapper.Map<ReviewDto>(reviewReturn),200);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO: {ex.Message}");
                return ServiceResult<ReviewDto>.FailResult("Ocorreu um erro inesperado ao adicionar uma revisão. Tente novamente mais tarde.", 500);
            }
        }
        public async Task<ServiceResult<ReviewDto>> PutReview(ReviewPutDto model)
        {
            try
            {
                if (model == null) return ServiceResult<ReviewDto>.FailResult("O Json está mal formatado.", 400);

                var personAuthenticaded = await _personAuthenticatedService.GetPersonAuthenticated();
                if (personAuthenticaded == null || personAuthenticaded.Active == false) return ServiceResult<ReviewDto>.FailResult("Você não possui autorização para realizar esta operação",401);

                var resultValidation = _reviewPutValidator.Validate(model);
                if (!resultValidation.IsValid) return ServiceResult<ReviewDto>.FailResult(resultValidation, 400);

                var review = await _reviewRepository.GetByHash(model.Hash);
                if (review == null) return ServiceResult<ReviewDto>.FailResult("A avaliação não foi encontrada na base de dados.", 404);
                if (review.Person.Id != personAuthenticaded.Id) return ServiceResult<ReviewDto>.FailResult("Você não possui autorização para realizar esta operação", 401);

                _mapper.Map(model, review);

                _allRepository.Update(review);
                await _allRepository.SaveChangesAsync();

                var reviewReturn = await _reviewRepository.GetByHash(review.Hash);

                return ServiceResult<ReviewDto>.SucessResult(_mapper.Map<ReviewDto>(reviewReturn), 200);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO: {ex.Message}");
                return ServiceResult<ReviewDto>.FailResult("Ocorreu um erro inesperado ao atualizar uma revisão. Tente novamente mais tarde.", 500);
            }
        }
        public async Task<ServiceResult<ReviewDto>> GetReview(Guid hashReview)
        {
            try
            {
                if (hashReview == Guid.Empty) return ServiceResult<ReviewDto>.FailResult("O hash não foi preenchido", 400);

                var personAuthenticaded = await _personAuthenticatedService.GetPersonAuthenticated();
                if (personAuthenticaded == null || personAuthenticaded.Active == false) return ServiceResult<ReviewDto>.FailResult("Você não possui autorização para realizar esta operação", 401);

                if (hashReview == Guid.Empty) return ServiceResult<ReviewDto>.FailResult("O hash não foi preenchido", 400);

                var review = await _reviewRepository.GetByHash(hashReview);
                if (review == null) return ServiceResult<ReviewDto>.FailResult("A avaliação não foi encontrado na base de dados.", 404);

                return ServiceResult<ReviewDto>.SucessResult(_mapper.Map<ReviewDto>(review), 200);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO: {ex.Message}");
                return ServiceResult<ReviewDto>.FailResult("Ocorreu um erro inesperado ao carregar uma revisão. Tente novamente mais tarde.", 500);
            }
        }

        public async Task<ServiceResult<ReviewDto>> GetReviewByMakerPerson(Guid hashMarker)
        {
            try
            {
                if (hashMarker == Guid.Empty) return ServiceResult<ReviewDto>.FailResult("O hash não foi preenchido", 400);

                var personAuthenticaded = await _personAuthenticatedService.GetPersonAuthenticated();
                if (personAuthenticaded == null || personAuthenticaded.Active == false) return ServiceResult<ReviewDto>.FailResult("Você não possui autorização para realizar esta operação", 401);


                var review = await _reviewRepository.GetByPersonMarker(personAuthenticaded.Id, hashMarker);
                if (review == null) return ServiceResult<ReviewDto>.FailResult("A avaliação não foi encontrada na base de dados.", 404);

                return ServiceResult<ReviewDto>.SucessResult(_mapper.Map<ReviewDto>(review), 200);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO: {ex.Message}");
                return ServiceResult<ReviewDto>.FailResult("Ocorreu um erro inesperado ao carregar uma revisão. Tente novamente mais tarde.", 500);
            }
        }

        public async Task<ServiceResult<ReviewDto>> DeleteReview(Guid hashReview)
        {
            try
            {
                if (hashReview == Guid.Empty) return ServiceResult<ReviewDto>.FailResult("O hash não foi preenchido", 400);

                var personAuthenticaded = await _personAuthenticatedService.GetPersonAuthenticated();
                if (personAuthenticaded == null || personAuthenticaded.Active == false) return ServiceResult<ReviewDto>.FailResult("Você não possui autorização para realizar esta operação", 401);


                var review = await _reviewRepository.GetByHash(hashReview);
                if (review == null) return ServiceResult<ReviewDto>.FailResult("A avaliação não foi encontrada na base de dados.", 404);

                if (review.Person.Hash != personAuthenticaded.Hash) return ServiceResult<ReviewDto>.FailResult("Você não possui autorização para realizar esta operação", 401);

                _allRepository.Delete(review);
                await _allRepository.SaveChangesAsync();

                return ServiceResult<ReviewDto>.SucessResult(200);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO: {ex.Message}");
                return ServiceResult<ReviewDto>.FailResult("Ocorreu um erro inesperado ao excluir uma revisão. Tente novamente mais tarde.", 500);
            }
        }
    }
}
