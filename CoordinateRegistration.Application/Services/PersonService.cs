using AutoMapper;
using CoordinateRegistration.Application.Dto;
using CoordinateRegistration.Application.Dto.Person;
using CoordinateRegistration.Application.Dto.TypeOccurrence;
using CoordinateRegistration.Application.Interface;
using CoordinateRegistration.Application.Interface.Authenticate;
using CoordinateRegistration.Application.Utils;
using CoordinateRegistration.Domain;
using CoordinateRegistration.Persistence.Interface;
using FluentValidation;


namespace CoordinateRegistration.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IAllRespository _allRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<PersonDeleteDto> _personDeleteValidator;
        private readonly IValidator<PersonAddDto> _personAddValidator;
        private readonly IValidator<PersonPutDto> _personPutValidator;
        private readonly IPersonAuthenticationService _personAuthenticatedService;


        public PersonService(IAllRespository allRepository, IPersonRepository personRepository, IMapper mapper, IValidator<PersonAddDto> personAddValidator, IValidator<PersonPutDto> personPutValidator, IPersonAuthenticationService personAuthenticatedService, IValidator<PersonDeleteDto> personDeleteValidator)
        {
            _allRepository = allRepository;
            _personRepository = personRepository;
            _mapper = mapper;
            _personAddValidator = personAddValidator;
            _personPutValidator = personPutValidator;
            _personAuthenticatedService = personAuthenticatedService;
            _personDeleteValidator = personDeleteValidator;
        }

        public async Task<ServiceResult<PersonDto>> CreatePerson(PersonAddDto model)
        {
            try
            {
                if (model == null) return ServiceResult<PersonDto>.FailResult("O Json está mal formatado.", 400);

                var resultValidator = _personAddValidator.Validate(model);

                if (!resultValidator.IsValid) return ServiceResult<PersonDto>.FailResult(resultValidator, 400);

                var personResult = await _personRepository.GetByEmail(model.Email);

                if (personResult != null) return ServiceResult<PersonDto>.FailResult("O usuário não pode ser criado com este e-mail.", 409);

                var person = _mapper.Map<Person>(model);
                person.Password = Cryptography.GetMd5(model.Password);

                _allRepository.Add(person);
                await _allRepository.SaveChangesAsync();

                var personReturn = await _personRepository.GetByHash(person.Hash);

                var personProfile = new PersonProfile();
                personProfile.ProfileId = 2;
                personProfile.PersonId = personReturn.Id;
                personProfile.Hash = Guid.NewGuid();

                _allRepository.Add(personProfile);
                await _allRepository.SaveChangesAsync();

                return ServiceResult<PersonDto>.SucessResult(_mapper.Map<PersonDto>(personReturn), 201);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO: {ex.Message}");
                return ServiceResult<PersonDto>.FailResult("Ocorreu um erro inesperado ao criar um usuário. Tente novamente mais tarde.", 500);
            }
        }
        public async Task<ServiceResult<PersonDto>> PutPerson(PersonPutDto model)
        {
            try
            {
                if (model == null) return ServiceResult<PersonDto>.FailResult("O Json está mal formatado.", 400);

                var resultValidator = _personPutValidator.Validate(model);

                if (!resultValidator.IsValid) return ServiceResult<PersonDto>.FailResult(resultValidator, 400);

                var personAuthenticaded = await _personAuthenticatedService.GetPersonAuthenticated();

                if (personAuthenticaded == null || personAuthenticaded.Active == false) return ServiceResult<PersonDto>.FailResult("Você não possui autorização para realizar esta operação", 401);

                var personResult = await _personRepository.GetByEmail(model.Email);

                if (personResult != null && (personAuthenticaded.Hash != personResult.Hash)) return ServiceResult<PersonDto>.FailResult("O usuário não pode ser alterado para este e-mail.", 409);

                _mapper.Map(model, personResult);

                _allRepository.Update(personResult);
                await _allRepository.SaveChangesAsync();

                var personReturn = await _personRepository.GetByHash(personResult.Hash);
                return ServiceResult<PersonDto>.SucessResult(_mapper.Map<PersonDto>(personReturn), 200);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO: {ex.Message}");
                return ServiceResult<PersonDto>.FailResult("Ocorreu um erro inesperado ao atualizar um usuário. Tente novamente mais tarde.", 500);
            }
        }
        public async Task<ServiceResult<PersonDto>> GetPersonByEmail(string model)
        {
            try
            {
                if (model == null) return ServiceResult<PersonDto>.FailResult("O Json está mal formatado.", 400);

                var personResult = await _personRepository.GetByEmail(model);
                if (personResult == null) return ServiceResult<PersonDto>.FailResult("O usuário não foi encontrado na base de dados.", 404);

                return ServiceResult<PersonDto>.SucessResult(_mapper.Map<PersonDto>(personResult), 200);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO: {ex.Message}");
                return ServiceResult<PersonDto>.FailResult("Ocorreu um erro inesperado ao carregar um usuário. Tente novamente mais tarde.", 500);
            }

        }
        public async Task<ServiceResult<PersonDto>> DeletePerson(PersonDeleteDto model)
        {

            try
            {
                if (model == null) return ServiceResult<PersonDto>.FailResult("O Json está mal formatado.", 400);

                var personAuthenticaded =  await _personAuthenticatedService.GetPersonAuthenticated();

                var resultValidator = _personDeleteValidator.Validate(model);

                if (!resultValidator.IsValid) return ServiceResult<PersonDto>.FailResult(resultValidator, 400);

                if (personAuthenticaded == null || personAuthenticaded.Active == false) return ServiceResult<PersonDto>.FailResult("Você não possui autorização para realizar esta operação", 401);

                if (Cryptography.GetMd5(model.Password) != personAuthenticaded.Password) return ServiceResult<PersonDto>.FailResult("A senha está incorreta", 400);


                personAuthenticaded.DateDeleted = DateTime.UtcNow;
                personAuthenticaded.Active = false;


                _allRepository.Update(personAuthenticaded);
                await _allRepository.SaveChangesAsync();

                return ServiceResult<PersonDto>.SucessResult(200);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO: {ex.Message}");
                return ServiceResult<PersonDto>.FailResult("Ocorreu um erro inesperado ao deletar um usuário. Tente novamente mais tarde.", 500);
            }


        }
        public async Task<ServiceResult<PersonDto>> DeletePersonForever(string email, PersonDeleteDto model)
        {
            try
            {
                if (model == null || email == null) return ServiceResult<PersonDto>.FailResult("O email não foi informado ou o Json está mal formatado.", 400);

                var personAuthenticaded = await _personAuthenticatedService.GetPersonAuthenticated();

                var resultValidator = _personDeleteValidator.Validate(model);

                if (!resultValidator.IsValid) return ServiceResult<PersonDto>.FailResult(resultValidator, 400);

               

                if (personAuthenticaded == null || personAuthenticaded.Active == false) return ServiceResult<PersonDto>.FailResult("Você não possui autorização para realizar esta operação", 401);
                if (Cryptography.GetMd5(model.Password) != personAuthenticaded.Password) return ServiceResult<PersonDto>.FailResult("A senha está incorreta", 401);

                var personToBeDeleted = await _personRepository.GetByEmail(email);

                if (personToBeDeleted == null) return ServiceResult<PersonDto>.FailResult("O usuário não foi encontrado na base de dados.", 400);


                _allRepository.Delete(personToBeDeleted);
                await _allRepository.SaveChangesAsync();

                return ServiceResult<PersonDto>.SucessResult(200);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO: {ex.Message}");
                return ServiceResult<PersonDto>.FailResult("Ocorreu um erro inesperado ao excluir um usuário. Tente novamente mais tarde.", 500);
            }
        }

    }
}
