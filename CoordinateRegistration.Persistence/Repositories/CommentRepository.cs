using CoordinateRegistration.Domain;
using CoordinateRegistration.Persistence.Context;
using CoordinateRegistration.Persistence.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoordinateRegistration.Persistence.Repositories
{
    public class CommentRepository : AllRespository, ICommentRepository
    {
        private readonly CoordinateRegistrationDbContext _context;
        public CommentRepository(CoordinateRegistrationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Comment> GetByHash(Guid hash)
        {
            return _context.Comment
                .Include(x => x.User)
                .Include(x => x.Marker)
                .FirstOrDefault(x => x.Hash.Equals(hash));
        }

        public async Task<IEnumerable<Comment>> GetByMarker(Guid hashMarker)
        {
            return _context.Comment
                .Include(x => x.User)
                .Include(x => x.Marker)
                .Where(x => x.Marker.Hash.Equals(hashMarker)).ToList();
        }

    }
}
