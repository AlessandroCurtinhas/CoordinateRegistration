using CoordinateRegistration.Domain;
using CoordinateRegistration.Persistence.Context;
using CoordinateRegistration.Persistence.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoordinateRegistration.Persistence.Repositories
{
    public class PersonRepository : AllRespository, IPersonRepository
    {
        private readonly CoordinateRegistrationDbContext _context;

        public PersonRepository(CoordinateRegistrationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Person> GetByEmail(string email)
        {
            return await _context.Person
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email);
        }
        public async Task<Person> GetByHash(Guid hash)
        {
            return await _context.Person
                .AsNoTracking()
                .Include(x => x.Profile)
                .ThenInclude(x => x.Profile)
                .FirstOrDefaultAsync(x => x.Hash == hash);
        }
        public async Task<Person> GetByEmailPassword(string email, string password)
        {
            return await _context.Person
                .AsNoTracking()
                .Include(x => x.Profile)
                .ThenInclude(x => x.Profile)
                .FirstOrDefaultAsync(u => u.Email.Equals(email) && u.Password.Equals(password) && u.Active == true);
        }

        public async Task<Person> GetByRecoveryHash(string? recoveryHash)
        {
            return await _context.Person
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.RecoveryHash.Equals(recoveryHash));
        }

    }
}
