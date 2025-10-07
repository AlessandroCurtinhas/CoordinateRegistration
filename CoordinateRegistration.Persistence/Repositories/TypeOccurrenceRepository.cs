using CoordinateRegistration.Domain;
using CoordinateRegistration.Persistence.Context;
using CoordinateRegistration.Persistence.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoordinateRegistration.Persistence.Repositories
{
    public class TypeOccurrenceRepository : AllRespository, ITypeOccurrenceRespository
    {

        private readonly CoordinateRegistrationDbContext _context;

        public TypeOccurrenceRepository(CoordinateRegistrationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TypeOccurrence>> GetAll()
        {
            return await _context.TypeOccurrence
                .AsNoTracking()
                .Include(x => x.UserCreate)
                .Include(x => x.UserUpdate)
                .Include(x => x.UserDelete)
                .ToListAsync();          
        }

        public async Task<IEnumerable<TypeOccurrence>> GetByHashes(List<TypeOccurrence> typeOccurrences)
        {
            var hashes = typeOccurrences.Select(x => x.Hash);

            return await _context.TypeOccurrence
                .AsNoTracking()
                .Include(x => x.UserCreate)
                .Include(x => x.UserUpdate)
                .Include(x => x.UserDelete)
                .Where(p => hashes.Contains(p.Hash))
                .ToListAsync();
        }


        public async Task<IEnumerable<int>> GetIdsByHash(List<Guid> hash)
        {
            return await _context.TypeOccurrence
                .AsNoTracking()
                .Where(p => hash.Contains(p.Hash) && p.Active.Equals(true))
                .Select(p => p.Id)
                .ToListAsync();
        }
        public async Task<IEnumerable<TypeOccurrence>> GetById(List<int> ids)
        {
            return await _context.TypeOccurrence
                .AsNoTracking()
                .Include(x => x.UserCreate)
                .Include(x => x.UserUpdate)
                .Include(x => x.UserDelete)
                .Where(x => x.Equals(ids))
                .ToListAsync();
        }

        public async Task<TypeOccurrence> GetByHash(Guid hash)
        {
            return await _context.TypeOccurrence
                .AsNoTracking()
                .Include(x => x.UserCreate)
                .Include(x => x.UserUpdate)
                .Include(x => x.UserDelete)
                .FirstOrDefaultAsync(x => x.Hash == hash);
        }
    }
}
