using CoordinateRegistration.Application.Dto;
using CoordinateRegistration.Application.Dto.Authentication;
using CoordinateRegistration.Application.Dto.User;
using CoordinateRegistration.Domain;

namespace CoordinateRegistration.Application.Interface.Authenticate
{
    public interface IUserAuthenticationService
    {
        Task<User?> GetUserAuthenticated();
        Task<ServiceResult<UserAuthenticatedDto>> Login(UserLoginDto model);
        Task<ServiceResult<UserDto>> RecoveryPasswordRequest(UserRecoveryRequestDto model);
        Task<ServiceResult<UserDto>> RecoveryPassword(Guid recoveryHash, UserRecoveryPasswordDto model);

    }
}
