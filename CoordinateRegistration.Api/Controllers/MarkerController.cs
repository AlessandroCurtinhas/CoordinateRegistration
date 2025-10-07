using CoordinateRegistration.Application.Dto;
using CoordinateRegistration.Application.Dto.Marker;
using CoordinateRegistration.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CoordinateRegistration.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class MarkerController : ControllerBase
    {
        private readonly IMarkerService _markerService;
        public MarkerController(IMarkerService markerService)
        {
            _markerService = markerService;
        }
        [HttpPost]
        public async Task<IActionResult> Post(MarkerAddDto model)
        {
            try
            {
                var marker = await _markerService.AddMarker(model);
                if (!marker.Success) return this.StatusCode(StatusCodes.Status422UnprocessableEntity, marker);
                return this.StatusCode(StatusCodes.Status201Created, marker);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<MarkerDto>.FailResult(ex.Message));
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put(MarkerPutDto model)
        {
            try
            {
                var marker = await _markerService.PutMarker(model);
                if (!marker.Success) return this.StatusCode(StatusCodes.Status422UnprocessableEntity, marker);
                return this.StatusCode(StatusCodes.Status200OK, marker);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<MarkerDto>.FailResult(ex.Message));
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMarker()
        {
            try
            {
                var markers = await _markerService.GetAllMarker();
                if (!markers.Success) return this.StatusCode(StatusCodes.Status422UnprocessableEntity, markers);
                if (markers.Success && markers.Data.IsNullOrEmpty()) return this.StatusCode(StatusCodes.Status204NoContent, markers);
                return this.StatusCode(StatusCodes.Status200OK, markers);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<MarkerDto>.FailResult(ex.Message));
            }
        }
        [HttpGet("{hash}")]
        public async Task<IActionResult> GetByHashMarker(Guid hash)
        {
            try
            {
                var marker = await _markerService.GetByHashMarker(hash);
                if (!marker.Success) return this.StatusCode(StatusCodes.Status422UnprocessableEntity, marker);              
                return this.StatusCode(StatusCodes.Status200OK, marker);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<MarkerDto>.FailResult(ex.Message));
            }
        }
        [HttpDelete("{hash}")]
        public async Task<IActionResult> DeleteMarker(Guid hash)
        {
            try
            {
                var marker = await _markerService.DeleteMarker(hash);
                if (!marker.Success) return this.StatusCode(StatusCodes.Status422UnprocessableEntity, marker);              
                return this.StatusCode(StatusCodes.Status200OK, marker);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<MarkerDto>.FailResult(ex.Message));
            }
        }

    }
}
