using AutoMapper;
using CoordinateRegistration.Application.JwtAuthentication;
using CoordinateRegistration.Application.Dto;
using CoordinateRegistration.Application.Dto.Authentication;
using CoordinateRegistration.Application.Interface.Authenticate;
using CoordinateRegistration.Application.Utils;
using CoordinateRegistration.Domain;
using CoordinateRegistration.Persistence.Interface;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using CoordinateRegistration.Application.Dto.Person;

namespace CoordinateRegistration.Application.Services.Authenticate;
public class PersonAuthenticationService : IPersonAuthenticationService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPersonRepository _personRepository;
    private readonly IValidator<PersonLoginDto> _personLoginValidator;
    private readonly JwtService _jwtService;
    private readonly IMapper _mapper;
    private readonly IValidator<PersonRecoveryRequestDto> _personRecoveryRequestValidator;
    private readonly IAllRespository _allRepository;
    private readonly IValidator<PersonRecoveryPasswordDto> _personRecoveryPasswordValidator;

    public PersonAuthenticationService(IHttpContextAccessor httpContextAccessor, IPersonRepository personRepository, JwtService jwtService, IValidator<PersonLoginDto> personLoginValidator, IMapper mapper, IValidator<PersonRecoveryRequestDto> personRecoveryRequestValidator, IAllRespository allRepository, IValidator<PersonRecoveryPasswordDto> personRecoveryPasswordValidator)
    {
        _httpContextAccessor = httpContextAccessor;
        _personRepository = personRepository;
        _jwtService = jwtService;
        _personLoginValidator = personLoginValidator;
        _mapper = mapper;
        _personRecoveryRequestValidator = personRecoveryRequestValidator;
        _allRepository = allRepository;
        _personRecoveryPasswordValidator = personRecoveryPasswordValidator;
    }
    public async Task<Person?> GetPersonAuthenticated()
    {
        var personHttpContext = new Person();
        var httpContext = _httpContextAccessor.HttpContext;

        personHttpContext.Hash = Guid.Parse(GetClaimValue(httpContext, "Hash"));
        //personHttpContext.Email = GetClaimValue(httpContext, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").ToString();

        if (personHttpContext.Hash == Guid.Empty)  return null;

        personHttpContext = await _personRepository.GetByHash(personHttpContext.Hash);

        return personHttpContext;

    }
    private string GetClaimValue(HttpContext httpContext, string claimType)
    {
        var person = httpContext.User;

        if (person.Identity.IsAuthenticated)
        {
            var claim = person.FindFirst(claimType);
            return claim?.Value;
        }

        return null;

    }
    public async Task<ServiceResult<PersonAuthenticatedDto>> Login(PersonLoginDto model)
    {
        try
        {
            if(model == null) return ServiceResult<PersonAuthenticatedDto>.FailResult("O Json está mal formatado.", 400);

            var resultValidation = _personLoginValidator.Validate(model);
            if (!resultValidation.IsValid) return ServiceResult<PersonAuthenticatedDto>.FailResult(resultValidation, 400);

            var password = Cryptography.GetMd5(model.Password);
            var person = await _personRepository.GetByEmailPassword(model.Email, password);

            if (person == null) return ServiceResult<PersonAuthenticatedDto>.FailResult("Email ou Senha inválidos.", 401);

            var token = _jwtService.GenerationToken(person);

            var personAuthorizationReturn = _mapper.Map<PersonAuthenticatedDto>(person);
            personAuthorizationReturn.AuthToken = token;

            return ServiceResult<PersonAuthenticatedDto>.SucessResult(personAuthorizationReturn, 200);
        }      
        catch (Exception ex)
        {
            Console.WriteLine($"ERRO: {ex.Message}");
            return ServiceResult<PersonAuthenticatedDto>.FailResult("Ocorreu um erro inesperado.", 500);

        }

    }
    public async Task<ServiceResult<PersonRecoveryRequestDto>> RecoveryPasswordRequest(PersonRecoveryRequestDto model)
    {
        try
        {
            if (model == null) return ServiceResult<PersonRecoveryRequestDto>.FailResult("O Json está mal formatado.", 400);

            var resultValidator = _personRecoveryRequestValidator.Validate(model);

            if (!resultValidator.IsValid) return ServiceResult<PersonRecoveryRequestDto>.FailResult(resultValidator, 400);

            var personResult = await _personRepository.GetByEmail(model.Email);
            if (personResult == null || personResult.Active == null) return ServiceResult<PersonRecoveryRequestDto>.SucessResult(200);

            personResult.PasswordDateRequest = DateTime.UtcNow;
            personResult.RecoveryHash = Guid.NewGuid();

            //implementar envio de email
            _allRepository.Update(personResult);
            await _allRepository.SaveChangesAsync();

            return ServiceResult<PersonRecoveryRequestDto>.SucessResult(200);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERRO: {ex.Message}");
            return ServiceResult<PersonRecoveryRequestDto>.FailResult("Ocorreu um erro inesperado.", 500);

        }
    }
    public async Task<ServiceResult<PersonDto>> RecoveryPassword(Guid? hash, PersonRecoveryPasswordDto model)
    {
        try
        {
            if (model == null) return ServiceResult<PersonDto>.FailResult("O Json está mal formatado.", 400);

            if (hash == null)  return ServiceResult<PersonDto>.FailResult("O hash deve ser informado.", 400);

            var personResulValidation = _personRecoveryPasswordValidator.Validate(model);

            if (!personResulValidation.IsValid) return ServiceResult<PersonDto>.FailResult(personResulValidation, 400);

            var personResult = await _personRepository.GetByRecoveryHash(hash);

            if (personResult == null) return ServiceResult<PersonDto>.FailResult("O usuário não foi encontrado na base de dados.", 404);     

            var date = personResult.PasswordDateRequest.GetValueOrDefault();
            var dateResult = date.AddMinutes(30) - DateTime.UtcNow;

            if (!(dateResult.TotalMilliseconds >= 0)) return ServiceResult<PersonDto>.FailResult("O tempo do token de recuperação de senha expirou.", 401);

            _mapper.Map(model, personResult);

            personResult.Password = Cryptography.GetMd5(model.Password);
            personResult.PasswordDateRequest = null;
            personResult.RecoveryHash = null;


            _allRepository.Update(personResult);
            await _allRepository.SaveChangesAsync();

            var personReturn = await _personRepository.GetByHash(personResult.Hash);
            return ServiceResult<PersonDto>.SucessResult(_mapper.Map<PersonDto>(personReturn), 200);

        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERRO: {ex.Message}");
            return ServiceResult<PersonDto>.FailResult("Ocorreu um erro inesperado.", 500);

        }

    }

}
