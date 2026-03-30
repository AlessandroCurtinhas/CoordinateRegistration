using AutoMapper;
using CoordinateRegistration.Application.Dto;
using CoordinateRegistration.Application.Dto.Marker;
using CoordinateRegistration.Application.Interface;
using CoordinateRegistration.Domain;
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
                var marker = await _markerService.AddMarker(model);
                if (!marker.Success) return this.StatusCode(marker.StatusCode, marker);
                return this.StatusCode(marker.StatusCode, marker);

        }
        [HttpPut]
        public async Task<IActionResult> Put(MarkerPutDto model)
        {
                var marker = await _markerService.PutMarker(model);
                if (!marker.Success) return this.StatusCode(marker.StatusCode, marker);
                return this.StatusCode(marker.StatusCode, marker);

        }
        [HttpGet("/Markers")]
        public async Task<IActionResult> GetAllMarker(DateTime dateStart, DateTime dateFinal)
        {
                var markers = await _markerService.GetAllMarker(dateStart, dateFinal);
                if (!markers.Success) return this.StatusCode(markers.StatusCode, markers);
                return this.StatusCode(markers.StatusCode, markers);

        }
        [HttpGet("{hash}")]
        public async Task<IActionResult> GetByHashMarker(Guid hash)
        {
                var marker = await _markerService.GetByHashMarker(hash);
                if (!marker.Success) return this.StatusCode(marker.StatusCode, marker);              
                return this.StatusCode(marker.StatusCode, marker);

        }
        [HttpDelete("{hash}")]
        public async Task<IActionResult> DeleteMarker(Guid hash)
        {
                var marker = await _markerService.DeleteMarker(hash);
                if (!marker.Success) return this.StatusCode(StatusCodes.Status422UnprocessableEntity, marker);              
                return this.StatusCode(StatusCodes.Status200OK, marker);

        }

    }
}
