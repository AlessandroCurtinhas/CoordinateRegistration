using CoordinateRegistration.Domain;
using CoordinateRegistration.Persistence.Context;
using CoordinateRegistration.Persistence.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoordinateRegistration.Persistence.Repositories
{
    public class ReviewRepository : AllRespository, IReviewRepository
    {
        private readonly CoordinateRegistrationDbContext _context;

        public ReviewRepository(CoordinateRegistrationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Review> GetByUserMarker (int userId, int markerId)
        {
            return  _context.Review
                .AsNoTracking()
                .Where(x => x.UserId.Equals(userId) && x.MarkerId.Equals(markerId))
                .FirstOrDefault();
        }

        public async Task<Review> GetByHash(Guid hash)
        {
            return await _context.Review
                .AsNoTracking()
                .Include(x => x.Marker)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Hash.Equals(hash));
        }

        public async Task<Review> GetByUserMarker(Guid userHash, Guid markerHash)
        {
            return _context.Review
                .AsNoTracking()
                .Include(x => x.Marker)
                .Include(x => x.User)
                .Where(x => x.User.Hash.Equals(userHash) && x.Marker.Hash.Equals(markerHash))
                .FirstOrDefault();
        }
        public async Task<Review> GetByUserMarker(int userid, Guid markerHash)
        {
            return _context.Review
                .AsNoTracking()
                .Include(x => x.Marker)
                .Include(x => x.User)
                .Where(x => x.User.Id.Equals(userid) && x.Marker.Hash.Equals(markerHash))
                .FirstOrDefault();
        }


    }
}
