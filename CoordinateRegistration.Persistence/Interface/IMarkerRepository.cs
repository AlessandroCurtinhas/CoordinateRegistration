using CoordinateRegistration.Domain;

namespace CoordinateRegistration.Persistence.Interface
{
    public interface IMarkerRepository : IAllRespository
    {
        Task<IEnumerable<Marker>> GetAll();
        Task<Marker> GetByHash(Guid hash);
    }
}
