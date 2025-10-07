using AutoMapper;
using CoordinateRegistration.Application.JwtAuthentication;
using CoordinateRegistration.Application.Dto;
using CoordinateRegistration.Application.Dto.Authentication;
using CoordinateRegistration.Application.Dto.User;
using CoordinateRegistration.Application.Interface.Authenticate;
using CoordinateRegistration.Application.Utils;
using CoordinateRegistration.Domain;
using CoordinateRegistration.Persistence.Interface;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CoordinateRegistration.Application.Services.Authenticate;
public class UserAuthenticationService : IUserAuthenticationService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserRepository _userRepository;
    private readonly IValidator<UserLoginDto> _userLoginValidator;
    private readonly JwtService _jwtService;
    private readonly IMapper _mapper;
    private readonly IValidator<UserRecoveryRequestDto> _userRecoveryRequestValidator;
    private readonly IAllRespository _allRepository;
    private readonly IValidator<UserRecoveryPasswordDto> _userRecoveryPasswordValidator;

    public UserAuthenticationService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository, JwtService jwtService, IValidator<UserLoginDto> userLoginValidator, IMapper mapper, IValidator<UserRecoveryRequestDto> userRecoveryRequestValidator, IAllRespository allRepository, IValidator<UserRecoveryPasswordDto> userRecoveryPasswordValidator)
    {
        _httpContextAccessor = httpContextAccessor;
        _userRepository = userRepository;
        _jwtService = jwtService;
        _userLoginValidator = userLoginValidator;
        _mapper = mapper;
        _userRecoveryRequestValidator = userRecoveryRequestValidator;
        _allRepository = allRepository;
        _userRecoveryPasswordValidator = userRecoveryPasswordValidator;
    }
    public async Task<User?> GetUserAuthenticated()
    {
        var userHttpContext = new User();
        var httpContext = _httpContextAccessor.HttpContext;

        userHttpContext.Hash = Guid.Parse(GetClaimValue(httpContext, "Hash"));
        //userHttpContext.Email = GetClaimValue(httpContext, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").ToString();

        if (userHttpContext.Hash == Guid.Empty)  return null;

        userHttpContext = await _userRepository.GetByHash(userHttpContext.Hash);

        return userHttpContext;

    }
    private string GetClaimValue(HttpContext httpContext, string claimType)
    {
        var user = httpContext.User;

        if (user.Identity.IsAuthenticated)
        {
            var claim = user.FindFirst(claimType);
            return claim?.Value;
        }

        return null;

    }
    public async Task<ServiceResult<UserAuthenticatedDto>> Login(UserLoginDto model)
    {
        try
        {
            var resultValidation = _userLoginValidator.Validate(model);
            if (!resultValidation.IsValid) return ServiceResult<UserAuthenticatedDto>.FailResult(resultValidation);

            var password = Cryptography.GetMd5(model.Password);
            var user = await _userRepository.GetByEmailPassword(model.Email, password);

            if (user == null) return ServiceResult<UserAuthenticatedDto>.FailResult("Email ou Senha inválidos.");

            var token = _jwtService.GenerationToken(user);

            var userAuthorizationReturn = _mapper.Map<UserAuthenticatedDto>(user);
            userAuthorizationReturn.AuthToken = token;

            return ServiceResult<UserAuthenticatedDto>.SucessResult(userAuthorizationReturn);
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }

    }
    public async Task<ServiceResult<UserDto>> RecoveryPasswordRequest(UserRecoveryRequestDto model)
    {
        try
        {
            var resultValidator = _userRecoveryRequestValidator.Validate(model);

            if (!resultValidator.IsValid) return ServiceResult<UserDto>.FailResult(resultValidator);

            var userResult = await _userRepository.GetByEmail(model.Email);
            //implementado desta forma, exibindo a base, para fins de testes e pela falta do serviço de mensagens
            if (userResult == null || userResult.Active == null) return ServiceResult<UserDto>.FailResult("O usuário não foi encontrado na base de dados.");

            userResult.PasswordDateRequest = DateTime.UtcNow;
            userResult.RecoveryHash = Guid.NewGuid();

            //implementar envio de email
            _allRepository.Update(userResult);
            await _allRepository.SaveChangesAsync();


            return ServiceResult<UserDto>.SucessResult();
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    public async Task<ServiceResult<UserDto>> RecoveryPassword(Guid recoveryHash, UserRecoveryPasswordDto model)
    {
        try
        {
            var userResult = await _userRepository.GetByRecoveryHash(recoveryHash);

            if (userResult == null) return ServiceResult<UserDto>.FailResult("O usuário não foi encontrado na base de dados.");

            var userResulValidation = _userRecoveryPasswordValidator.Validate(model);

            if (!userResulValidation.IsValid) return ServiceResult<UserDto>.FailResult(userResulValidation);

            var date = userResult.PasswordDateRequest.GetValueOrDefault();
            var dateResult = date.AddMinutes(30) - DateTime.UtcNow;

            if (!(dateResult.Minutes >= 0)) return ServiceResult<UserDto>.FailResult("O tempo de token de recuperação de senha espirou.");



            _mapper.Map(model, userResult);

            userResult.Password = Cryptography.GetMd5(model.Password);
            userResult.PasswordDateRequest = null;
            userResult.RecoveryHash = null;


            _allRepository.Update(userResult);
            await _allRepository.SaveChangesAsync();

            var userReturn = await _userRepository.GetByHash(userResult.Hash);
            return ServiceResult<UserDto>.SucessResult(_mapper.Map<UserDto>(userReturn));

        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }

    }

}
