using CoordinateRegistration.Domain;

namespace CoordinateRegistration.Persistence.Interface
{
    public interface ICommentRepository : IAllRespository
    {
        Task<Comment> GetByHash(Guid hash);
        Task<IEnumerable<Comment>> GetByMarker(Guid hashMarker);
    }
}
