using CoordinateRegistration.Application.Dto;
using CoordinateRegistration.Application.Dto.Marker;

namespace CoordinateRegistration.Application.Interface
{
    public interface IMarkerService
    {
        Task<ServiceResult<IEnumerable<MarkerDto>>> GetAllMarker();
        Task<ServiceResult<MarkerDto>> AddMarker(MarkerAddDto model);
        Task<ServiceResult<MarkerDto>> PutMarker(MarkerPutDto model);
        Task<ServiceResult<MarkerDto>> DeleteMarker(Guid hash);
        Task<ServiceResult<MarkerDto>> GetByHashMarker(Guid hash);
    }
}
