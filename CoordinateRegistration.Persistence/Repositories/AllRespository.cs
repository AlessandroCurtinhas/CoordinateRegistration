using CoordinateRegistration.Persistence.Context;
using CoordinateRegistration.Persistence.Interface;

namespace CoordinateRegistration.Persistence.Repositories
{
    public class AllRespository : IAllRespository
    {
        private readonly CoordinateRegistrationDbContext _context;

        public AllRespository(CoordinateRegistrationDbContext context)
        {
            _context = context;

        }

        public void Add<T>(T entity) where T : class
        {
            _context.AddAsync(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {

            return (await _context.SaveChangesAsync()) > 0;
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
    }
}
