using CoordinateRegistration.Application.Dto;
using CoordinateRegistration.Application.Dto.Authentication;
using CoordinateRegistration.Application.Dto.Person;
using CoordinateRegistration.Domain;

namespace CoordinateRegistration.Application.Interface.Authenticate
{
    public interface IPersonAuthenticationService
    {
        Task<Person?> GetPersonAuthenticated();
        Task<ServiceResult<PersonAuthenticatedDto>> Login(PersonLoginDto model);
        Task<ServiceResult<PersonRecoveryRequestDto>> RecoveryPasswordRequest(PersonRecoveryRequestDto model);
        Task<ServiceResult<PersonDto>> RecoveryPassword(Guid? hash, PersonRecoveryPasswordDto model);

    }
}
