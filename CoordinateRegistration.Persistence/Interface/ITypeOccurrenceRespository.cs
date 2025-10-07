using CoordinateRegistration.Domain;

namespace CoordinateRegistration.Persistence.Interface
{
    public interface ITypeOccurrenceRespository : IAllRespository
    {
        Task<IEnumerable<TypeOccurrence>> GetAll();
        Task<IEnumerable<int>> GetIdsByHash(List<Guid> hash);
        Task<IEnumerable<TypeOccurrence>> GetById(List<int> ids);
        Task<IEnumerable<TypeOccurrence>> GetByHashes(List<TypeOccurrence> typeOccurrences);
        Task<TypeOccurrence> GetByHash(Guid hash);

    }
}
