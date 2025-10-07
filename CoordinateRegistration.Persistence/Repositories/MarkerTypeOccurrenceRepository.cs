using CoordinateRegistration.Domain;
using CoordinateRegistration.Persistence.Context;
using CoordinateRegistration.Persistence.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoordinateRegistration.Persistence.Repositories
{
    public class MarkerTypeOccurrenceRepository : AllRespository, IMarkerTypeOccurrenceRepository
    {

        private readonly CoordinateRegistrationDbContext _context;
        public MarkerTypeOccurrenceRepository(CoordinateRegistrationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MarkerTypeOccurrence>> GetAll()
        {
            return await _context.MarkerTypeOccurrence
                .Include(x => x.TypeOccurrence)
                .Include(y => y.Marker)
                .ToListAsync();
        }
    }
}
