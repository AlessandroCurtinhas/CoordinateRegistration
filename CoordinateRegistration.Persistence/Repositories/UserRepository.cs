using CoordinateRegistration.Domain;
using CoordinateRegistration.Persistence.Context;
using CoordinateRegistration.Persistence.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoordinateRegistration.Persistence.Repositories
{
    public class UserRepository : AllRespository, IUserRepository
    {
        private readonly CoordinateRegistrationDbContext _context;

        public UserRepository(CoordinateRegistrationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<User> GetByEmail(string email)
        {
            return await _context.User
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email);
        }
        public async Task<User> GetByHash(Guid hash)
        {
            return await _context.User
                .AsNoTracking()
                .Include(x => x.Profile)
                .ThenInclude(x => x.Profile)
                .FirstOrDefaultAsync(x => x.Hash == hash);
        }
        public async Task<User> GetByEmailPassword(string email, string password)
        {
            return await _context.User
                .AsNoTracking()
                .Include(x => x.Profile)
                .ThenInclude(x => x.Profile)
                .FirstOrDefaultAsync(u => u.Email.Equals(email) && u.Password.Equals(password) && u.Active == true);
        }

        public async Task<User> GetByRecoveryHash(Guid recoveryHash)
        {
            return await _context.User
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.RecoveryHash.Equals(recoveryHash));
        }

    }
}
