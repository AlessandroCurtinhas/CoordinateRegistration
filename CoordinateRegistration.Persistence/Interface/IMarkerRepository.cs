using CoordinateRegistration.Domain;

namespace CoordinateRegistration.Persistence.Interface
{
    public interface IMarkerRepository : IAllRespository
    {
        Task<IEnumerable<Marker>> GetAll(DateTime dateStart, DateTime dateFinal);
        Task<Marker> GetByHash(Guid hash);
    }
}
