using CoordinateRegistration.Application.Dto.Authentication;
using FluentValidation;

namespace CoordinateRegistration.Application.Validators.Authentication
{
    public class UserRecoveryRequestValidator : AbstractValidator<UserRecoveryRequestDto>
    {
        public UserRecoveryRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("O email deve ser preenchido.")
                .MinimumLength(5)
                .MaximumLength(150)
                .EmailAddress();
        }
    }
}
