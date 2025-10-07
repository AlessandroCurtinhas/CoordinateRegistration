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
        private readonly IUserAuthenticationService _userAuthenticatedService;
        private readonly ICommentRepository _commentRepository;
        private readonly IMarkerRepository _markerRepository;
        private readonly IAllRespository _allRepository;

        private readonly IMapper _mapper;
        public CommentService(IValidator<CommentAddDto> commentAddValidator, IUserAuthenticationService userAuthenticatedService, IMapper mapper, IValidator<CommentPutDto> commentPutValidator, ICommentRepository commentRepository, IMarkerRepository markerRepository, IAllRespository allRepository)
        {
            _commentAddValidator = commentAddValidator;
            _userAuthenticatedService = userAuthenticatedService;
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
                var userAuthenticaded = await _userAuthenticatedService.GetUserAuthenticated();
                if (userAuthenticaded == null || userAuthenticaded.Active == false) return ServiceResult<CommentDto>.FailResult("Você não possui autorização para realizar esta operação");

                var resultValidator = await _commentAddValidator.ValidateAsync(model);
                if (!resultValidator.IsValid) return ServiceResult<CommentDto>.FailResult(resultValidator);

                var marker = await _markerRepository.GetByHash(model.MarkerHash);
                if (marker == null) return ServiceResult<CommentDto>.FailResult("O marcador não foi encontrado na base de dados.");

                var comment = _mapper.Map<Comment>(model);
                comment.MarkerId = marker.Id;
                comment.UserId = userAuthenticaded.Id;

                _allRepository.Add(comment);
                await _allRepository.SaveChangesAsync();

                var commentReturn = await _commentRepository.GetByHash(comment.Hash);

                return ServiceResult<CommentDto>.SucessResult(_mapper.Map<CommentDto>(commentReturn));

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResult<CommentDto>> PutComment(CommentPutDto model)
        {
            try
            {
                var userAuthenticaded = await _userAuthenticatedService.GetUserAuthenticated();
                if (userAuthenticaded == null || userAuthenticaded.Active == false) return ServiceResult<CommentDto>.FailResult("Você não possui autorização para realizar esta operação");

                var resultValidator = await _commentPutValidator.ValidateAsync(model);
                if (!resultValidator.IsValid) return ServiceResult<CommentDto>.FailResult(resultValidator);               

                var comment = await _commentRepository.GetByHash(model.Hash);

                if (comment == null) return ServiceResult<CommentDto>.FailResult("O comentário não foi encontrado na base de dados");          

                if (comment.UserId != userAuthenticaded.Id ) return ServiceResult<CommentDto>.FailResult("Você não possui autorização para realizar esta operação");


                var marker = await _markerRepository.GetByHash(model.MarkerHash);
                if (marker == null) return ServiceResult<CommentDto>.FailResult("O marcador não foi encontrado na base de dados.");

                if(comment.MarkerId != marker.Id ) return ServiceResult<CommentDto>.FailResult("O marcador do comentário não pode ser alterado.");

                _mapper.Map(model, comment);

                _allRepository.Update(comment);
                await _allRepository.SaveChangesAsync();

                var commentReturn = await _commentRepository.GetByHash(comment.Hash);

                return ServiceResult<CommentDto>.SucessResult(_mapper.Map<CommentDto>(commentReturn));

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResult<IEnumerable<CommentDto>>> GetComments(Guid hashMarker)
        {
            try
            {
                if (hashMarker == Guid.Empty) return ServiceResult<IEnumerable<CommentDto>>.FailResult("O hash não foi preenchido");

                var userAuthenticaded = await _userAuthenticatedService.GetUserAuthenticated();
                if (userAuthenticaded == null || userAuthenticaded.Active == false) return ServiceResult<IEnumerable<CommentDto>>.FailResult("Você não possui autorização para realizar esta operação");

                var comments = await _commentRepository.GetByMarker(hashMarker);

                return ServiceResult<IEnumerable<CommentDto>>.SucessResult(_mapper.Map<IEnumerable<CommentDto>>(comments));

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<ServiceResult<CommentDto>> DeleteComment(Guid hashComment)
        {
            try
            {
                if (hashComment == Guid.Empty) return ServiceResult<CommentDto>.FailResult("O hash não foi preenchido");

                var userAuthenticaded = await _userAuthenticatedService.GetUserAuthenticated();
                if (userAuthenticaded == null || userAuthenticaded.Active == false) return ServiceResult<CommentDto>.FailResult("Você não possui autorização para realizar esta operação");

                var comment = await _commentRepository.GetByHash(hashComment);
                if (comment == null) return ServiceResult<CommentDto>.FailResult("A avaliação não foi encontrada na base de dados.");

                if (comment.User.Id!= userAuthenticaded.Id ) return ServiceResult<CommentDto>.FailResult("Você não possui autorização para realizar esta operação");

                _allRepository.Delete(comment);
                await _allRepository.SaveChangesAsync();

                return ServiceResult<CommentDto>.SucessResult();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
