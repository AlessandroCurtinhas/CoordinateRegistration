using CoordinateRegistration.Domain;
using CoordinateRegistration.Persistence.Context;
using CoordinateRegistration.Persistence.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoordinateRegistration.Persistence.Repositories
{
    public class MarkerRepository : AllRespository, IMarkerRepository
    {
        private readonly CoordinateRegistrationDbContext _context;

        public MarkerRepository(CoordinateRegistrationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Marker>> GetAll()
        {
            return await _context.Marker
                .OrderByDescending(m => m.DateCreated)
                .Include(m => m.Reviews)
                .Include(m => m.User)
                .Include(x => x.MarkerTypeOccurrences.Where(i => i.TypeOccurrence.Active == true))
                    .ThenInclude(x => x.TypeOccurrence)
                .ToListAsync();
        }

        public async Task<Marker> GetByHash(Guid hash)
        {
            return await _context.Marker
                .Include(x => x.User)
                .Include(m => m.Reviews)
                .Include(x => x.MarkerTypeOccurrences)
                    .ThenInclude(x => x.TypeOccurrence)
                .FirstOrDefaultAsync(x => x.Hash == hash);

        }
    }
}

       
    
