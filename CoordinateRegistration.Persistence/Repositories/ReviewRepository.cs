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

        public async Task<Review> GetByPersonMarker (int PersonId, int markerId)
        {
            return  _context.Review
                .AsNoTracking()
                .Where(x => x.PersonId.Equals(PersonId) && x.MarkerId.Equals(markerId))
                .FirstOrDefault();
        }

        public async Task<Review> GetByHash(Guid hash)
        {
            return await _context.Review
                .AsNoTracking()
                .Include(x => x.Marker)
                .Include(x => x.Person)
                .FirstOrDefaultAsync(x => x.Hash.Equals(hash));
        }

        public async Task<Review> GetByPersonMarker(Guid PersonHash, Guid markerHash)
        {
            return _context.Review
                .AsNoTracking()
                .Include(x => x.Marker)
                .Include(x => x.Person)
                .Where(x => x.Person.Hash.Equals(PersonHash) && x.Marker.Hash.Equals(markerHash))
                .FirstOrDefault();
        }
        public async Task<Review> GetByPersonMarker(int PersonId, Guid markerHash)
        {
            return _context.Review
                .AsNoTracking()
                .Include(x => x.Marker)
                .Include(x => x.Person)
                .Where(x => x.Person.Id.Equals(PersonId) && x.Marker.Hash.Equals(markerHash))
                .FirstOrDefault();
        }


    }
}
