using AutoMapper;
using CoordinateRegistration.Application.Dto;
using CoordinateRegistration.Application.Dto.TypeOccurrence;
using CoordinateRegistration.Application.Interface;
using CoordinateRegistration.Application.Interface.Authenticate;
using CoordinateRegistration.Domain;
using CoordinateRegistration.Persistence.Interface;
using FluentValidation;
using FluentValidation.Results;

namespace CoordinateRegistration.Application.Services
{
    public class TypeOccurrenceService : ITypeOccurrenceService
    {
        private readonly ITypeOccurrenceRespository _typeOccurrenceRepository;
        private readonly IAllRespository _allRepository;
        private readonly IValidator<TypeOccurrenceAddDto> _typeOccurrenceAddValidator;
        private readonly IValidator<TypeOccurrencePutDto> _typeOccurrencePutValidator;
        private readonly IUserAuthenticationService _userAuthenticatedService;
        private readonly IMapper _mapper;

        public TypeOccurrenceService(ITypeOccurrenceRespository typeOccurrenceRepository, IMapper mapper, IValidator<TypeOccurrenceAddDto> typeOccurrenceAddValidator, IAllRespository allRepository, IValidator<TypeOccurrencePutDto> typeOccurrencePutValidator, IUserAuthenticationService userAuthenticatedService)
        {
            _typeOccurrenceRepository = typeOccurrenceRepository;
            _mapper = mapper;
            _typeOccurrenceAddValidator = typeOccurrenceAddValidator;
            _allRepository = allRepository;
            _typeOccurrencePutValidator = typeOccurrencePutValidator;
            _userAuthenticatedService = userAuthenticatedService;
        }
        public async Task<ServiceResult<IEnumerable<TypeOccurrenceDto>>> GetAllTypeOccurrence()
        {
            try
            {
                var typeOccurrences = await _typeOccurrenceRepository.GetAll();
                return ServiceResult<IEnumerable<TypeOccurrenceDto>>.SucessResult(_mapper.Map<IEnumerable<TypeOccurrenceDto>>(typeOccurrences));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ServiceResult<IEnumerable<TypeOccurrenceDtoUser>>> GetAllTypeOccurrenceFilter()
        {
            try
            {
                var typeOccurrences = await _typeOccurrenceRepository.GetAll();

                typeOccurrences = typeOccurrences.Where(x => x.Active.Equals(true));

                return ServiceResult<IEnumerable<TypeOccurrenceDtoUser>>.SucessResult(_mapper.Map<IEnumerable<TypeOccurrenceDtoUser>>(typeOccurrences));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ServiceResult<IEnumerable<TypeOccurrenceDto>>> PostTypeOccurrences(IEnumerable<TypeOccurrenceAddDto> model)
        {         
            try
            {
                var userAuthenticaded = await _userAuthenticatedService.GetUserAuthenticated();
                if (userAuthenticaded == null || userAuthenticaded.Active == false) return ServiceResult<IEnumerable<TypeOccurrenceDto>>.FailResult("Você não possui autorização para realizar esta operação");


                var validations = new List<ValidationResult>();
                var typeOccurrences = new List<TypeOccurrence>();

                foreach (var item in model)
                {
                    var result = _typeOccurrenceAddValidator.Validate(item);
                    if (!result.IsValid) validations.Add(result);

                }

                if (validations.Any()) return ServiceResult<IEnumerable<TypeOccurrenceDto>>
                        .FailResult("Ocorreram vários erros de preenchimento");//to do
               

                foreach (var item in model)
                {
                    var typeOccurrence = _mapper.Map<TypeOccurrence>(item);
                    typeOccurrence.UserId = userAuthenticaded.Id;
                    typeOccurrences.Add(typeOccurrence);
                    _allRepository.Add(typeOccurrence);
                }

                await _allRepository.SaveChangesAsync();

                var typesOccurrences = await _typeOccurrenceRepository.GetByHashes(typeOccurrences);

                return ServiceResult<IEnumerable<TypeOccurrenceDto>>.SucessResult(_mapper.Map<IEnumerable<TypeOccurrenceDto>>(typesOccurrences));

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<ServiceResult<TypeOccurrenceDto>> DeleteTypeOccurrence(Guid hash)
        {
            try
            {
                var userAuthenticaded = await _userAuthenticatedService.GetUserAuthenticated();
                if (userAuthenticaded == null || userAuthenticaded.Active == false) return ServiceResult<TypeOccurrenceDto>.FailResult("Você não possui autorização para realizar esta operação");

                var typeOccurrence = await ValidatorTypeOccurrence(hash);

                if (typeOccurrence == null) return ServiceResult<TypeOccurrenceDto>.FailResult("O tipo de ocorrência não foi encontrado na base de dados.");

                _allRepository.Delete(typeOccurrence);
                await _allRepository.SaveChangesAsync();

                return ServiceResult<TypeOccurrenceDto>.SucessResult();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        public async Task<ServiceResult<TypeOccurrenceDto>> GetByHashTypeOccurrence(Guid hash)
        {
            try
            {
                var userAuthenticaded = await _userAuthenticatedService.GetUserAuthenticated();
                if (userAuthenticaded == null || userAuthenticaded.Active == false) return ServiceResult<TypeOccurrenceDto>.FailResult("Você não possui autorização para realizar esta operação");

                var typeOccurrence = await ValidatorTypeOccurrence(hash);
                if (typeOccurrence == null) return ServiceResult<TypeOccurrenceDto>.FailResult("O tipo de ocorrência não foi encontrado na base de dados.");
                return ServiceResult<TypeOccurrenceDto>.SucessResult(_mapper.Map<TypeOccurrenceDto>(typeOccurrence));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        public async Task<ServiceResult<TypeOccurrenceDto>> PutTypeOccurrence (TypeOccurrencePutDto model)
        {
            try
            {
                var userAuthenticaded = await _userAuthenticatedService.GetUserAuthenticated();
                if (userAuthenticaded == null || userAuthenticaded.Active == false) return ServiceResult<TypeOccurrenceDto>.FailResult("Você não possui autorização para realizar esta operação");

                var result = await _typeOccurrencePutValidator.ValidateAsync(model);
                if (!result.IsValid) return ServiceResult<TypeOccurrenceDto>.FailResult(result);

                var typeOccurrence = await ValidatorTypeOccurrence(model.Hash);
                if (typeOccurrence == null) return ServiceResult<TypeOccurrenceDto>.FailResult("O tipo de ocorrência não foi encontrado na base de dados.");

                _mapper.Map(model, typeOccurrence);

                typeOccurrence.UserUpdateId = userAuthenticaded.Id;

                if (model.Active == false) 
                {
                    typeOccurrence.Active = false;
                    typeOccurrence.UserDeleteId = userAuthenticaded.Id;
                    typeOccurrence.DateDeleted = DateTime.UtcNow;
                }

                _allRepository.Update(typeOccurrence);
                await _allRepository.SaveChangesAsync();


                var typeOccurrenceReturn = await _typeOccurrenceRepository.GetByHash(typeOccurrence.Hash);

                return ServiceResult<TypeOccurrenceDto>.SucessResult(_mapper.Map<TypeOccurrenceDto>(typeOccurrenceReturn));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        private async Task<TypeOccurrence> ValidatorTypeOccurrence(Guid request)
        {

            return await _typeOccurrenceRepository.GetByHash(request);
            
        }
    }
}
