using CoordinateRegistration.Application.Dto;
using CoordinateRegistration.Application.Dto.Person;

namespace CoordinateRegistration.Application.Interface
{
    public interface IPersonService
    {
        Task<ServiceResult<PersonDto>> CreatePerson(PersonAddDto model);
        Task<ServiceResult<PersonDto>> PutPerson(PersonPutDto model);
        Task<ServiceResult<PersonDto>> GetPersonByEmail(string model);
        Task<ServiceResult<PersonDto>> DeletePerson(PersonDeleteDto model);
        Task<ServiceResult<PersonDto>> DeletePersonForever(string email, PersonDeleteDto passwordAdmin);
    }
}
