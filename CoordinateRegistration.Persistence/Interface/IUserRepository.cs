using CoordinateRegistration.Domain;

namespace CoordinateRegistration.Persistence.Interface
{
    public interface IUserRepository : IAllRespository
    {
        Task<User> GetByEmail(string email);
        Task<User> GetByHash(Guid hash);
        Task<User> GetByEmailPassword(string email, string password);
        Task<User> GetByRecoveryHash(Guid recoveryHash);
    }
}
