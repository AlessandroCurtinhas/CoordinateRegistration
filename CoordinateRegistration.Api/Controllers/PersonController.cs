using CoordinateRegistration.Application.Dto.Person;
using CoordinateRegistration.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoordinateRegistration.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreatePerson([FromBody] PersonAddDto model)
        {
                var person = await _personService.CreatePerson(model);
                if (!person.Success) return this.StatusCode(person.StatusCode, person);
                return this.StatusCode(person.StatusCode, person);

        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PersonPutDto model)
        {
                var person = await _personService.PutPerson(model);
                if (!person.Success) return this.StatusCode(person.StatusCode, person);
                return this.StatusCode(person.StatusCode, person);

        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{email}")]
        public async Task<IActionResult> GetPersonByEmail(string email)
        {
                var person = await _personService.GetPersonByEmail(email);
                if (!person.Success) return this.StatusCode(person.StatusCode, person);
                return this.StatusCode(person.StatusCode, person);

        }
        [HttpDelete("{email}")]
        public async Task<IActionResult> DeletePerson(PersonDeleteDto password)
        {
                var person = await _personService.DeletePerson(password);
                if (!person.Success) return this.StatusCode(person.StatusCode, person);
                return this.StatusCode(person.StatusCode, person);

        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("/person/deletepersonforever/{emailDelForever}")]
        public async Task<IActionResult> DeletePersonForever(string emailDelForever, PersonDeleteDto passwordAdmin)
        {
                var person = await _personService.DeletePersonForever(emailDelForever, passwordAdmin);
                if (!person.Success) return this.StatusCode(person.StatusCode, person);
                return this.StatusCode(person.StatusCode, person);

        }
    }
   
}

