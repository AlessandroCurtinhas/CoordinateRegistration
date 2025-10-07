using CoordinateRegistration.Application.Dto;
using CoordinateRegistration.Application.Dto.Marker;
using CoordinateRegistration.Application.Dto.TypeOccurrence;
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
    public class TypeOccurrenceController : ControllerBase
    {
        private readonly ITypeOccurrenceService _typeOccurrenceService;

        public TypeOccurrenceController(ITypeOccurrenceService typeOccurrenceService)
        {
            _typeOccurrenceService = typeOccurrenceService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllTypeOccurrence()
        {
            try
            {
                var typeOcurrences = await _typeOccurrenceService.GetAllTypeOccurrence();
                if (!typeOcurrences.Success) return StatusCode(StatusCodes.Status422UnprocessableEntity, typeOcurrences);
                if (typeOcurrences.Success && typeOcurrences.Data.IsNullOrEmpty()) return this.StatusCode(StatusCodes.Status204NoContent, typeOcurrences);                
                return StatusCode(StatusCodes.Status200OK, typeOcurrences);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<TypeOccurrenceDto>.FailResult(ex.Message));
            }
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("/AllTypeOccurrenceFilter")]
        public async Task<IActionResult> GetAllTypeOccurrenceFilter()
        {
            try
            {
                var typeOcurrences = await _typeOccurrenceService.GetAllTypeOccurrenceFilter();
                if (!typeOcurrences.Success) return StatusCode(StatusCodes.Status422UnprocessableEntity, typeOcurrences);
                if (typeOcurrences.Success && typeOcurrences.Data.IsNullOrEmpty()) return this.StatusCode(StatusCodes.Status204NoContent, typeOcurrences);
                return StatusCode(StatusCodes.Status200OK, typeOcurrences);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<TypeOccurrenceDto>.FailResult(ex.Message));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddTypeOccurrence(IEnumerable<TypeOccurrenceAddDto> model)
        {
            try
            {
                var typeOcurrences = await _typeOccurrenceService.PostTypeOccurrences(model);
                if (!typeOcurrences.Success) return StatusCode(StatusCodes.Status422UnprocessableEntity, typeOcurrences);           
                return StatusCode(StatusCodes.Status200OK, typeOcurrences);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<TypeOccurrenceDto>.FailResult(ex.Message));
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{hash}")]       
        public async Task<IActionResult> DeleteTypeOccurrence(Guid hash)
        {
            try
            {
                var typeOccurrence = await _typeOccurrenceService.DeleteTypeOccurrence(hash);
                if (!typeOccurrence.Success) return this.StatusCode(StatusCodes.Status422UnprocessableEntity, typeOccurrence);
                return this.StatusCode(StatusCodes.Status200OK, typeOccurrence);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<TypeOccurrence>.FailResult(ex.Message));
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{hash}")]
        public async Task<IActionResult> GetByHashTypeOccurrence(Guid hash)
        {
            try
            {
                var typeOccurrence = await _typeOccurrenceService.GetByHashTypeOccurrence(hash);
                if (!typeOccurrence.Success) return this.StatusCode(StatusCodes.Status422UnprocessableEntity, typeOccurrence);
                return this.StatusCode(StatusCodes.Status200OK, typeOccurrence);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<MarkerDto>.FailResult(ex.Message));
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Put(TypeOccurrencePutDto model)
        {
            try
            {
                var typeOccurrence = await _typeOccurrenceService.PutTypeOccurrence(model);
                if (!typeOccurrence.Success) return this.StatusCode(StatusCodes.Status422UnprocessableEntity, typeOccurrence);
                return this.StatusCode(StatusCodes.Status200OK, typeOccurrence);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<MarkerDto>.FailResult(ex.Message));
            }
        }

    }
}
