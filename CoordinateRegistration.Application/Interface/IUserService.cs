using CoordinateRegistration.Application.Dto;
using CoordinateRegistration.Application.Dto.User;

namespace CoordinateRegistration.Application.Interface
{
    public interface IUserService
    {
        Task<ServiceResult<UserDto>> CreateUser(UserAddDto model);
        Task<ServiceResult<UserDto>> PutUser(UserPutDto model);
        Task<ServiceResult<UserDto>> GetUserByEmail(string model);
        Task<ServiceResult<UserDto>> DeleteUser(UserDeleteDto model);
    }
}
