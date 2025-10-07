using CoordinateRegistration.Application.Dto;
using CoordinateRegistration.Application.Dto.Marker;
using CoordinateRegistration.Application.Dto.User;
using CoordinateRegistration.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoordinateRegistration.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser(UserAddDto model)
        {
            try
            {
                var user = await _userService.CreateUser(model);
                if (!user.Success) return this.StatusCode(StatusCodes.Status422UnprocessableEntity, user);
                return this.StatusCode(StatusCodes.Status201Created, user);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<MarkerDto>.FailResult(ex.Message));
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put(UserPutDto model)
        {
            try
            {
                var user = await _userService.PutUser(model);
                if (!user.Success) return this.StatusCode(StatusCodes.Status422UnprocessableEntity, user);
                return this.StatusCode(StatusCodes.Status200OK, user);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<MarkerDto>.FailResult(ex.Message));
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            try
            {
                var user = await _userService.GetUserByEmail(email);
                if (!user.Success) return this.StatusCode(StatusCodes.Status422UnprocessableEntity, user);
                return this.StatusCode(StatusCodes.Status200OK, user);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<MarkerDto>.FailResult(ex.Message));
            }
        }
        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteUser(UserDeleteDto email)
        {
            try
            {
                var user = await _userService.DeleteUser(email);
                if (!user.Success) return this.StatusCode(StatusCodes.Status422UnprocessableEntity, user);
                return this.StatusCode(StatusCodes.Status200OK, user);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<MarkerDto>.FailResult(ex.Message));
            }
        }
    }
   
}

