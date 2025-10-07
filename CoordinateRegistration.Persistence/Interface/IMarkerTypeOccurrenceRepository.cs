using CoordinateRegistration.Domain;

namespace CoordinateRegistration.Persistence.Interface
{
    public interface IMarkerTypeOccurrenceRepository: IAllRespository
    {
        Task<IEnumerable<MarkerTypeOccurrence>> GetAll();
    }
}