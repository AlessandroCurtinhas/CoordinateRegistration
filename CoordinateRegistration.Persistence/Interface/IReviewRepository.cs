using CoordinateRegistration.Domain;

namespace CoordinateRegistration.Persistence.Interface
{
    public interface IReviewRepository
    {
        Task<Review> GetByUserMarker(int userId, int markerId);
        Task<Review> GetByHash(Guid hash);
        Task<Review> GetByUserMarker(Guid userHash, Guid markerHash);
        Task<Review> GetByUserMarker(int userid, Guid markerHash);
    }
}
