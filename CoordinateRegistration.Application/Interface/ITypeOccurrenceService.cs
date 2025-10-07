using CoordinateRegistration.Application.Dto;
using CoordinateRegistration.Application.Dto.TypeOccurrence;

namespace CoordinateRegistration.Application.Interface
{
    public interface ITypeOccurrenceService
    {
        Task<ServiceResult<IEnumerable<TypeOccurrenceDto>>> GetAllTypeOccurrence();
        Task<ServiceResult<IEnumerable<TypeOccurrenceDto>>> PostTypeOccurrences(IEnumerable<TypeOccurrenceAddDto> model);
        Task<ServiceResult<TypeOccurrenceDto>> DeleteTypeOccurrence(Guid hash);
        Task<ServiceResult<TypeOccurrenceDto>> GetByHashTypeOccurrence(Guid hash);
        Task<ServiceResult<TypeOccurrenceDto>> PutTypeOccurrence(TypeOccurrencePutDto model);
        Task<ServiceResult<IEnumerable<TypeOccurrenceDtoUser>>> GetAllTypeOccurrenceFilter();

    }
}
