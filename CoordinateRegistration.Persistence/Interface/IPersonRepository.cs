using CoordinateRegistration.Domain;

namespace CoordinateRegistration.Persistence.Interface
{
    public interface IPersonRepository : IAllRespository
    {
        Task<Person> GetByEmail(string email);
        Task<Person> GetByHash(Guid hash);
        Task<Person> GetByEmailPassword(string email, string password);
        Task<Person> GetByRecoveryHash(Guid? recoveryHash);
    }
}
