using CoordinateRegistration.Application.Dto.Person;
using FluentValidation;

namespace CoordinateRegistration.Application.Validators.Person
{
    public class PersonDeleteValidator : AbstractValidator<PersonDeleteDto>
    {
        public PersonDeleteValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("A senha deve ser preenchida.");

            RuleFor(x => x.ConfirmedPassword)
                .NotEmpty()
                .WithMessage("A confirmção de senha deve ser preenchido.");


            RuleFor(person => person.ConfirmedPassword)
                .Equal(person => person.Password).WithMessage("As senhas devem ser iguais.");
        }
    }
}
