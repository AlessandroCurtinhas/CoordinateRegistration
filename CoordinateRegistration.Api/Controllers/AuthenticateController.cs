using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CoordinateRegistration.Application.Interface.Authenticate;
using CoordinateRegistration.Application.Dto.Authentication;

namespace CoordinateRegistration.Controllers
{
    public class AuthenticateController : Controller
    {
        
        private readonly IPersonAuthenticationService _personAuthenticatedService;

        public AuthenticateController(IPersonAuthenticationService personAuthenticatedService)
        {
            _personAuthenticatedService = personAuthenticatedService;
        }

        [HttpPost("/login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] PersonLoginDto model)
        {
                var token = await _personAuthenticatedService.Login(model);
                if (!token.Success) return this.StatusCode(token.StatusCode,token);
                return this.StatusCode(token.StatusCode, token);

        }

        [HttpPost("/recoveryPasswordRequest")]
        [AllowAnonymous]
        public async Task<IActionResult> RecoveryPasswordRequest([FromBody] PersonRecoveryRequestDto model)
        {
                var token = await _personAuthenticatedService.RecoveryPasswordRequest(model);
                if (!token.Success) return this.StatusCode(token.StatusCode, token);
                return this.StatusCode(token.StatusCode, token);

        }

        [HttpPost("/recoveryPassword/{hash}")]
        [AllowAnonymous]
        public async Task<IActionResult> RecoveryPassword(Guid hash, [FromBody] PersonRecoveryPasswordDto model)
        {
                var user = await _personAuthenticatedService.RecoveryPassword(hash, model);
                if (!user.Success) return this.StatusCode(user.StatusCode, user);
                return this.StatusCode(user.StatusCode, user);

        }
    }
}
