using CoordinateRegistration.Application.Dto.Person;
using FluentValidation;

namespace CoordinateRegistration.Application.Validators.Person
{
    public class PersonPutValidator : AbstractValidator<PersonPutDto>
    {
        public PersonPutValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("O comentário deve ser preenchido.")
                .MinimumLength(5)
                .MaximumLength(150);

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("O email deve ser preenchido.")
                .MinimumLength(5)
                .MaximumLength(150)
                .EmailAddress();
        }
    }
}
