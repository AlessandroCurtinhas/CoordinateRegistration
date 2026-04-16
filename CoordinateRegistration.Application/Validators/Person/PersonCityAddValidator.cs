using CoordinateRegistration.Application.Dto.Person;
using FluentValidation;

namespace CoordinateRegistration.Application.Validators.Person
{
    public class PersonCityAddValidator : AbstractValidator<PersonAddCityDto>
    {
        public PersonCityAddValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("O nome da cidade deve ser preenchido.")
                .MaximumLength(150);

            RuleFor(x => x.State)
                .NotEmpty()
                .WithMessage("O nome do estado deve ser preenchido.")
                .MaximumLength(150);

            RuleFor(x => x.UF)
                .NotEmpty().WithMessage("A Unidade Federativa (UF) do estado deve ser preenchido.")
                .Matches("^[A-Z]+$").WithMessage("A Unidade Federativa (UF) deve ser preenchida apenas letras maiúsculas.")
                .Length(2);

        }
    }
}
