using CoordinateRegistration.Application.Dto.Authentication;
using FluentValidation;

namespace CoordinateRegistration.Application.Validators.Authentication
{
    public class PersonRecoveryRequestValidator : AbstractValidator<PersonRecoveryRequestDto>
    {
        public PersonRecoveryRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("O email deve ser preenchido.")
                .MinimumLength(3)
                .MaximumLength(150)
                .EmailAddress().WithMessage("O email informado é inválido.");
        }
    }
}
