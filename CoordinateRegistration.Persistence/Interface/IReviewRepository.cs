using CoordinateRegistration.Domain;

namespace CoordinateRegistration.Persistence.Interface
{
    public interface IReviewRepository
    {
        Task<Review> GetByPersonMarker(int personId, int markerId);
        Task<Review> GetByHash(Guid hash);
        Task<Review> GetByPersonMarker(Guid personHash, Guid markerHash);
        Task<Review> GetByPersonMarker(int personid, Guid markerHash);
    }
}
