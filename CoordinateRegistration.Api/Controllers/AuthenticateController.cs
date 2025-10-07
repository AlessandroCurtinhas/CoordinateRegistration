using CoordinateRegistration.Application.Dto.Marker;
using CoordinateRegistration.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CoordinateRegistration.Application.Interface.Authenticate;
using CoordinateRegistration.Application.Dto.Authentication;

namespace CoordinateRegistration.Controllers
{
    public class AuthenticateController : Controller
    {
        
        private readonly IUserAuthenticationService _userAuthenticatedService;

        public AuthenticateController(IUserAuthenticationService userAuthenticatedService)
        {
            _userAuthenticatedService = userAuthenticatedService;
        }

        [HttpPost("/login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto model)
        {
            try
            {
                var token = await _userAuthenticatedService.Login(model);
                if (!token.Success) return this.StatusCode(StatusCodes.Status422UnprocessableEntity, token);
                return this.StatusCode(StatusCodes.Status200OK, token);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<MarkerDto>.FailResult(ex.Message));
            }
        }

        [HttpPost("/recoveryPasswordRequest")]
        [AllowAnonymous]
        public async Task<IActionResult> RecoveryPasswordRequest(UserRecoveryRequestDto model)
        {
            try
            {
                var token = await _userAuthenticatedService.RecoveryPasswordRequest(model);
                if (!token.Success) return this.StatusCode(StatusCodes.Status422UnprocessableEntity, token);
                return this.StatusCode(StatusCodes.Status200OK, token);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<MarkerDto>.FailResult(ex.Message));
            }
        }

        [HttpPost("/recoveryPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> RecoveryPassword(Guid hashRecovery, UserRecoveryPasswordDto model)
        {
            try
            {
                var user = await _userAuthenticatedService.RecoveryPassword(hashRecovery, model);
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
