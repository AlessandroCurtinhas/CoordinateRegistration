using AutoMapper;
using CoordinateRegistration.Application.Dto;
using CoordinateRegistration.Application.Dto.Comment;
using CoordinateRegistration.Application.Interface;
using CoordinateRegistration.Application.Interface.Authenticate;
using CoordinateRegistration.Domain;
using CoordinateRegistration.Persistence.Interface;
using FluentValidation;

namespace CoordinateRegistration.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly IValidator<CommentAddDto> _commentAddValidator;
        private readonly IValidator<CommentPutDto> _commentPutValidator;
        private readonly IPersonAuthenticationService _personAuthenticatedService;
        private readonly ICommentRepository _commentRepository;
        private readonly IMarkerRepository _markerRepository;
        private readonly IAllRespository _allRepository;

        private readonly IMapper _mapper;
        public CommentService(IValidator<CommentAddDto> commentAddValidator, IPersonAuthenticationService personAuthenticatedService, IMapper mapper, IValidator<CommentPutDto> commentPutValidator, ICommentRepository commentRepository, IMarkerRepository markerRepository, IAllRespository allRepository)
        {
            _commentAddValidator = commentAddValidator;
            _personAuthenticatedService = personAuthenticatedService;
            _mapper = mapper;
            _commentPutValidator = commentPutValidator;
            _commentRepository = commentRepository;
            _markerRepository = markerRepository;
            _allRepository = allRepository;
        }

        public async Task<ServiceResult<CommentDto>> AddComment(CommentAddDto model)
        {
            try
            {
                if (model == null) return ServiceResult<CommentDto>.FailResult("O Json está mal formatado.", 400);

                var personAuthenticaded = await _personAuthenticatedService.GetPersonAuthenticated();
                if (personAuthenticaded == null || personAuthenticaded.Active == false) return ServiceResult<CommentDto>.FailResult("Você não possui autorização para realizar esta operação", 401);

                var resultValidator = await _commentAddValidator.ValidateAsync(model);
                if (!resultValidator.IsValid) return ServiceResult<CommentDto>.FailResult(resultValidator, 400);

                var marker = await _markerRepository.GetByHash(model.MarkerHash);
                if (marker == null) return ServiceResult<CommentDto>.FailResult("O marcador não foi encontrado na base de dados.", 404);

                var comment = _mapper.Map<Comment>(model);
                comment.MarkerId = marker.Id;
                comment.PersonId = personAuthenticaded.Id;

                _allRepository.Add(comment);
                await _allRepository.SaveChangesAsync();

                var commentReturn = await _commentRepository.GetByHash(comment.Hash);

                return ServiceResult<CommentDto>.SucessResult(_mapper.Map<CommentDto>(commentReturn),201);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO: {ex.Message}");
                return ServiceResult<CommentDto>.FailResult("Ocorreu um erro inesperado ao salvar o comentário. Tente novamente mais tarde.", 500);

            }
        }

        public async Task<ServiceResult<CommentDto>> PutComment(CommentPutDto model)
        {
            try
            {
                if (model == null) return ServiceResult<CommentDto>.FailResult("O Json está mal formatado.", 400);

                var personAuthenticaded = await _personAuthenticatedService.GetPersonAuthenticated();
                if (personAuthenticaded == null || personAuthenticaded.Active == false) return ServiceResult<CommentDto>.FailResult("Você não possui autorização para realizar esta operação", 401);

                var resultValidator = await _commentPutValidator.ValidateAsync(model);
                if (!resultValidator.IsValid) return ServiceResult<CommentDto>.FailResult(resultValidator,400);               

                var comment = await _commentRepository.GetByHash(model.Hash);

                if (comment == null) return ServiceResult<CommentDto>.FailResult("O comentário não foi encontrado na base de dados", 404);          

                if (comment.PersonId != personAuthenticaded.Id ) return ServiceResult<CommentDto>.FailResult("Você não possui autorização para realizar esta operação", 401);


                var marker = await _markerRepository.GetByHash(model.MarkerHash);
                if (marker == null) return ServiceResult<CommentDto>.FailResult("O marcador não foi encontrado na base de dados.", 404);

                if(comment.MarkerId != marker.Id ) return ServiceResult<CommentDto>.FailResult("O marcador do comentário não pode ser alterado.", 401);

                _mapper.Map(model, comment);

                _allRepository.Update(comment);
                await _allRepository.SaveChangesAsync();

                var commentReturn = await _commentRepository.GetByHash(comment.Hash);

                return ServiceResult<CommentDto>.SucessResult(_mapper.Map<CommentDto>(commentReturn), 200);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO: {ex.Message}");
                return ServiceResult<CommentDto>.FailResult("Ocorreu um erro inesperado ao atualizar o comentário. Tente novamente mais tarde.", 500);

            }
        }

        public async Task<ServiceResult<IEnumerable<CommentDto>>> GetComments(Guid hashMarker)
        {
            try
            {

                if (hashMarker == Guid.Empty) return ServiceResult<IEnumerable<CommentDto>>.FailResult("O hash não foi preenchido", 400);

                var personAuthenticaded = await _personAuthenticatedService.GetPersonAuthenticated();
                if (personAuthenticaded == null || personAuthenticaded.Active == false) return ServiceResult<IEnumerable<CommentDto>>.FailResult("Você não possui autorização para realizar esta operação", 401);

                var comments = await _commentRepository.GetByMarker(hashMarker);

                if(comments.Count() == 0) return ServiceResult<IEnumerable<CommentDto>>.SucessResult(_mapper.Map<IEnumerable<CommentDto>>(comments), 204);

                return ServiceResult<IEnumerable<CommentDto>>.SucessResult(_mapper.Map<IEnumerable<CommentDto>>(comments),200);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO: {ex.Message}");
                return ServiceResult<IEnumerable<CommentDto>>.FailResult("Ocorreu um erro inesperado ao carregar os comentários. Tente novamente mais tarde.", 500);

            }


        }
        public async Task<ServiceResult<CommentDto>> DeleteComment(Guid hashComment)
        {
            try
            {
                if (hashComment == Guid.Empty) return ServiceResult<CommentDto>.FailResult("O hash não foi preenchido", 400);

                var personAuthenticaded = await _personAuthenticatedService.GetPersonAuthenticated();
                if (personAuthenticaded == null || personAuthenticaded.Active == false) return ServiceResult<CommentDto>.FailResult("Você não possui autorização para realizar esta operação", 401);

                var comment = await _commentRepository.GetByHash(hashComment);
                if (comment == null) return ServiceResult<CommentDto>.FailResult("A avaliação não foi encontrada na base de dados.", 404);

                if (comment.Person.Id!= personAuthenticaded.Id ) return ServiceResult<CommentDto>.FailResult("Você não possui autorização para realizar esta operação", 401);

                _allRepository.Delete(comment);
                await _allRepository.SaveChangesAsync();

                return ServiceResult<CommentDto>.SucessResult(200);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO: {ex.Message}");
                return ServiceResult<CommentDto>.FailResult("Ocorreu um erro inesperado ao excluir um comentário. Tente novamente mais tarde.", 500);

            }
        }
    }
}
