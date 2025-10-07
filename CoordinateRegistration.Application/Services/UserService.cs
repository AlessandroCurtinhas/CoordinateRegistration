using AutoMapper;
using CoordinateRegistration.Application.Dto;
using CoordinateRegistration.Application.Dto.User;
using CoordinateRegistration.Application.Interface;
using CoordinateRegistration.Application.Interface.Authenticate;
using CoordinateRegistration.Application.Utils;
using CoordinateRegistration.Domain;
using CoordinateRegistration.Persistence.Interface;
using FluentValidation;


namespace CoordinateRegistration.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAllRespository _allRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<UserDeleteDto> _userDeleteValidator;
        private readonly IValidator<UserAddDto> _userAddValidator;
        private readonly IValidator<UserPutDto> _userPutValidator;
        private readonly IUserAuthenticationService _userAuthenticatedService;


        public UserService(IAllRespository allRepository, IUserRepository userRepository, IMapper mapper, IValidator<UserAddDto> userAddValidator, IValidator<UserPutDto> userPutValidator, IUserAuthenticationService userAuthenticatedService, IValidator<UserDeleteDto> userDeleteValidator)
        {
            _allRepository = allRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _userAddValidator = userAddValidator;
            _userPutValidator = userPutValidator;
            _userAuthenticatedService = userAuthenticatedService;
            _userDeleteValidator = userDeleteValidator;
        }

        public async Task<ServiceResult<UserDto>> CreateUser(UserAddDto model)
        {
            try
            {
                var resultValidator = _userAddValidator.Validate(model);

                if (!resultValidator.IsValid) return ServiceResult<UserDto>.FailResult(resultValidator);

                var userResult = await _userRepository.GetByEmail(model.Email);

                if (userResult != null) return ServiceResult<UserDto>.FailResult("O usuário não pode ser criado com este e-mail.");

                var user = _mapper.Map<User>(model);
                user.Password = Cryptography.GetMd5(model.Password);

                _allRepository.Add(user);
                await _allRepository.SaveChangesAsync();

                var userReturn = await _userRepository.GetByHash(user.Hash);

                var userProfile = new UserProfile();
                userProfile.ProfileId = 2;
                userProfile.UserId = userReturn.Id;
                userProfile.Hash = Guid.NewGuid();

                _allRepository.Add(userProfile);
                await _allRepository.SaveChangesAsync();

                return ServiceResult<UserDto>.SucessResult(_mapper.Map<UserDto>(userReturn));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }
        public async Task<ServiceResult<UserDto>> PutUser(UserPutDto model)
        {
            try
            {

                var resultValidator = _userPutValidator.Validate(model);

                if (!resultValidator.IsValid) return ServiceResult<UserDto>.FailResult(resultValidator);

                var userAuthenticaded = await _userAuthenticatedService.GetUserAuthenticated();

                if (userAuthenticaded == null || userAuthenticaded.Active == false) return ServiceResult<UserDto>.FailResult("Você não possui autorização para realizar esta operação");

                var userResult = await _userRepository.GetByEmail(model.Email);

                if (userResult != null && (userAuthenticaded.Hash != userResult.Hash)) return ServiceResult<UserDto>.FailResult("O usuário não pode ser alterado para este e-mail.");

                _mapper.Map(model, userResult);

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
        public async Task<ServiceResult<UserDto>> GetUserByEmail(string model)
        {
            try
            {
                var userResult = await _userRepository.GetByEmail(model);
                if (userResult == null) return ServiceResult<UserDto>.FailResult("O usuário não foi encontrado na base de dados.");

                return ServiceResult<UserDto>.SucessResult(_mapper.Map<UserDto>(userResult));

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        public async Task<ServiceResult<UserDto>> DeleteUser(UserDeleteDto model)
        {

            try
            {

                var userAuthenticaded =  await _userAuthenticatedService.GetUserAuthenticated();

                var resultValidator = _userDeleteValidator.Validate(model);

                if (!resultValidator.IsValid) return ServiceResult<UserDto>.FailResult(resultValidator);

                if (userAuthenticaded == null || userAuthenticaded.Active == null) return ServiceResult<UserDto>.FailResult("Você não possui autorização para realizar esta operação");

                if (Cryptography.GetMd5(model.Password) != userAuthenticaded.Password) return ServiceResult<UserDto>.FailResult("A senha está incorreta");


                userAuthenticaded.DateDeleted = DateTime.UtcNow;
                userAuthenticaded.Active = false;


                _allRepository.Update(userAuthenticaded);
                await _allRepository.SaveChangesAsync();

                return ServiceResult<UserDto>.SucessResult();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }

    }
}
