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
                var typeOcurrences = await _typeOccurrenceService.GetAllTypeOccurrence();
                if (!typeOcurrences.Success) return StatusCode(typeOcurrences.StatusCode, typeOcurrences);       
                return StatusCode(typeOcurrences.StatusCode, typeOcurrences);

        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("/AllTypeOccurrenceFilter")]
        public async Task<IActionResult> GetAllTypeOccurrenceFilter()
        {
                var typeOcurrences = await _typeOccurrenceService.GetAllTypeOccurrenceFilter();
                if (!typeOcurrences.Success) return StatusCode(typeOcurrences.StatusCode, typeOcurrences);
                return StatusCode(typeOcurrences.StatusCode, typeOcurrences);

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddTypeOccurrence(IEnumerable<TypeOccurrenceAddDto> model)
        {
                var typeOcurrences = await _typeOccurrenceService.PostTypeOccurrences(model);
                if (!typeOcurrences.Success) return StatusCode(typeOcurrences.StatusCode, typeOcurrences);           
                return StatusCode(typeOcurrences.StatusCode, typeOcurrences);

        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{hash}")]       
        public async Task<IActionResult> DeleteTypeOccurrence(Guid hash)
        {
                var typeOccurrence = await _typeOccurrenceService.DeleteTypeOccurrence(hash);
                if (!typeOccurrence.Success) return this.StatusCode(typeOccurrence.StatusCode, typeOccurrence);
                return this.StatusCode(typeOccurrence.StatusCode, typeOccurrence);

        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{hash}")]
        public async Task<IActionResult> GetByHashTypeOccurrence(Guid hash)
        {
                var typeOccurrence = await _typeOccurrenceService.GetByHashTypeOccurrence(hash);
                if (!typeOccurrence.Success) return this.StatusCode(typeOccurrence.StatusCode, typeOccurrence);
                return this.StatusCode(typeOccurrence.StatusCode, typeOccurrence);

        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Put(TypeOccurrencePutDto model)
        {
                var typeOccurrence = await _typeOccurrenceService.PutTypeOccurrence(model);
                if (!typeOccurrence.Success) return this.StatusCode(typeOccurrence.StatusCode, typeOccurrence);
                return this.StatusCode(typeOccurrence.StatusCode, typeOccurrence);

        }

    }
}
