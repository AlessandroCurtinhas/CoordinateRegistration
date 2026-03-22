using AutoMapper;
using CoordinateRegistration.Application.Dto;
using CoordinateRegistration.Application.Dto.Comment;
using CoordinateRegistration.Application.Dto.Marker;
using CoordinateRegistration.Application.Interface;
using CoordinateRegistration.Application.Interface.Authenticate;
using CoordinateRegistration.Domain;
using CoordinateRegistration.Persistence.Interface;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace CoordinateRegistration.Application.Services
{
    public class MarkerService : IMarkerService
    {
        private readonly IAllRespository _allRepository;
        private readonly IMarkerRepository _markerRepository;
        private readonly IValidator<MarkerAddDto> _markerAddValidator;
        private readonly IValidator<MarkerPutDto> _markerPutValidator;
        private readonly ITypeOccurrenceRespository _typeOccurrenceRepository;
        private readonly IPersonAuthenticationService _personAuthenticatedService;
        private readonly IMapper _mapper;

        public MarkerService(IAllRespository allRepository, IMarkerRepository markerRepository, ITypeOccurrenceRespository typeOccurrenceRespository, IValidator<MarkerAddDto> markerAddValidator, IValidator<MarkerPutDto> markerPutValidator, IMapper mapper, IPersonAuthenticationService personAuthenticatedService)
        {
            _allRepository = allRepository;
            _typeOccurrenceRepository = typeOccurrenceRespository;
            _markerPutValidator = markerPutValidator;
            _markerRepository = markerRepository;
            _markerAddValidator = markerAddValidator;
            _mapper = mapper;
            _personAuthenticatedService = personAuthenticatedService;
        }
        public async Task<ServiceResult<MarkerDto>> AddMarker(MarkerAddDto model)
        {
            try
            {
                if (model == null) return ServiceResult<MarkerDto>.FailResult("O Json está mal formatado.", 400);

                var personAuthenticaded = await _personAuthenticatedService.GetPersonAuthenticated();
                if (personAuthenticaded == null || personAuthenticaded.Active == false) return ServiceResult<MarkerDto>.FailResult("Você não possui autorização para realizar esta operação",401);              

                var resultValidator = await _markerAddValidator.ValidateAsync(model);
                if (!resultValidator.IsValid) return ServiceResult<MarkerDto>.FailResult(resultValidator, 400);

                var typeOccurrenceIds = await ValidatorTypeOccurrence(model.TypeOccurrenceHash.ToList());
                if (typeOccurrenceIds.IsNullOrEmpty()) return ServiceResult<MarkerDto>.FailResult("Uma ou mais ocorrências informadas não existem na base de dados.", 404);


                var marker = _mapper.Map<Marker>(model);
                marker.PersonId = personAuthenticaded.Id;

                var markerTypesOccurrences = new List<MarkerTypeOccurrence>();

                foreach (var id in typeOccurrenceIds)
                {
                    var markerTypesOccurrence = new MarkerTypeOccurrence
                    {
                        TypeOccurrenceId = id,
                        DateCreated = DateTime.UtcNow,
                        Hash = Guid.NewGuid()
                    };

                    markerTypesOccurrences.Add(markerTypesOccurrence);

                }

                marker.MarkerTypeOccurrences = markerTypesOccurrences;

                _allRepository.Add(marker);
                await _allRepository.SaveChangesAsync();

                var markerReturn = await _markerRepository.GetByHash(marker.Hash);
                return ServiceResult<MarkerDto>.SucessResult(_mapper.Map<MarkerDto>(markerReturn), 201);


            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO: {ex.Message}");
                return ServiceResult<MarkerDto>.FailResult("Ocorreu um erro inesperado.", 500);

            }

        }
        public async Task<ServiceResult<MarkerDto>> DeleteMarker(Guid hash)
        {

            try
            {
                if (hash == Guid.Empty) return ServiceResult<MarkerDto>.FailResult("O hash não foi preenchido", 400);

                var personAuthenticaded = await _personAuthenticatedService.GetPersonAuthenticated();
                if (personAuthenticaded == null || personAuthenticaded.Active == false) return ServiceResult<MarkerDto>.FailResult("Você não possui autorização para realizar esta operação", 401);

                var marker = await ValidadorMarker(hash);
                if (marker == null) return ServiceResult<MarkerDto>.FailResult("O marcador não foi encontrado na base de dados.", 404);

                if (marker.Person.Id != personAuthenticaded.Id) return ServiceResult<MarkerDto>.FailResult("Você não possui autorização para realizar esta operação", 401);

                _allRepository.Delete(marker);
                await _allRepository.SaveChangesAsync();

                return ServiceResult<MarkerDto>.SucessResult(200);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO: {ex.Message}");
                return ServiceResult<MarkerDto>.FailResult("Ocorreu um erro inesperado.", 500);

            }

        }
        public async Task<ServiceResult<MarkerDto>> PutMarker(MarkerPutDto model)
        {
            try
            {
                if (model == null) return ServiceResult<MarkerDto>.FailResult("O Json está mal formatado.", 400);

                var personAuthenticaded = await _personAuthenticatedService.GetPersonAuthenticated();
                if (personAuthenticaded == null || personAuthenticaded.Active == false) return ServiceResult<MarkerDto>.FailResult("Você não possui autorização para realizar esta operação", 401);              

                var result = await _markerPutValidator.ValidateAsync(model);
                if (!result.IsValid) return ServiceResult<MarkerDto>.FailResult(result, 400);

                var marker = await ValidadorMarker(model.Hash);
                if (marker == null) return ServiceResult<MarkerDto>.FailResult("O marcador não foi encontrado na base de dados.", 404);

                if (personAuthenticaded.Id != marker.Person.Id) return ServiceResult<MarkerDto>.FailResult("Você não possui autorização para realizar esta operação", 401);

                var typeOccurrenceIds = await ValidatorTypeOccurrence(model.TypeOccurrenceHash.ToList());
                if (typeOccurrenceIds.IsNullOrEmpty()) return ServiceResult<MarkerDto>.FailResult("Uma ou mais Tipo de Ocorrências informadas não existem na base de dados.", 404);           

                var markerTypesOccurrences = new List<MarkerTypeOccurrence>();
               
                foreach (var id in typeOccurrenceIds)
                {
                    var markerTypesOccurrence = new MarkerTypeOccurrence
                    {
                        TypeOccurrenceId = id,
                        DateCreated = DateTime.UtcNow,
                        Hash = Guid.NewGuid()
                    };

                    markerTypesOccurrences.Add(markerTypesOccurrence);

                }

                _mapper.Map(model, marker);
                marker.MarkerTypeOccurrences = markerTypesOccurrences;

                _allRepository.Update(marker);
                await _allRepository.SaveChangesAsync();

                var markerReturn = await _markerRepository.GetByHash(marker.Hash);
                return ServiceResult<MarkerDto>.SucessResult(_mapper.Map<MarkerDto>(markerReturn),200);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO: {ex.Message}");
                return ServiceResult<MarkerDto>.FailResult("Ocorreu um erro inesperado.", 500);

            }


        }
        public async Task<ServiceResult<IEnumerable<MarkerDto>>> GetAllMarker()
        {
            try
            {
                var personAuthenticaded = await _personAuthenticatedService.GetPersonAuthenticated();
                if (personAuthenticaded == null || personAuthenticaded.Active == false) return ServiceResult<IEnumerable<MarkerDto>>.FailResult("Você não possui autorização para realizar esta operação", 401);

                var markers = await _markerRepository.GetAll();

                if(markers.Count() == 0) return ServiceResult<IEnumerable<MarkerDto>>.SucessResult(_mapper.Map<IEnumerable<MarkerDto>>(markers), 204);

                return ServiceResult<IEnumerable<MarkerDto>>.SucessResult(_mapper.Map<IEnumerable<MarkerDto>>(markers),200);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO: {ex.Message}");
                return ServiceResult<IEnumerable<MarkerDto>>.FailResult("Ocorreu um erro inesperado.", 500);

            }
        }
        public async Task<ServiceResult<MarkerDto>> GetByHashMarker(Guid hash)
        {           
            try
            {
                if (hash == Guid.Empty) return ServiceResult<MarkerDto>.FailResult("O hash não foi preenchido", 400);

                var personAuthenticaded = await _personAuthenticatedService.GetPersonAuthenticated();
                if (personAuthenticaded == null || personAuthenticaded.Active == false) return ServiceResult<MarkerDto>.FailResult("Você não possui autorização para realizar esta operação", 401);

                var marker = await ValidadorMarker(hash);
                if(marker == null) return ServiceResult<MarkerDto>.FailResult("O marcador não foi encontrado na base de dados.", 404);
                return ServiceResult<MarkerDto>.SucessResult(_mapper.Map<MarkerDto>(marker), 200);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO: {ex.Message}");
                return ServiceResult<MarkerDto>.FailResult("Ocorreu um erro inesperado.", 500);

            }

        }
        private async Task<IEnumerable<int>> ValidatorTypeOccurrence(List<Guid> request)
        {          
            var occurrences = await _typeOccurrenceRepository.GetIdsByHash(request);
            var occurrencesRequest = request.Distinct().Count();

            if ((occurrences.Count() != occurrencesRequest)) return null;

            return occurrences;
        }
        private async Task<Marker> ValidadorMarker(Guid request)
        {
            return await _markerRepository.GetByHash(request); 
        }

    }
}
