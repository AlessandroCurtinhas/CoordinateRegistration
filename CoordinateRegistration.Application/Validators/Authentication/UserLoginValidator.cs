using CoordinateRegistration.Application.Dto.Authentication;
using FluentValidation;

namespace CoordinateRegistration.Application.Validators.Authentication
{
    public class UserLoginValidator : AbstractValidator<UserLoginDto>
    {

        public UserLoginValidator()
        {

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("O email deve ser preenchido.")
                .MinimumLength(3)
                .MaximumLength(150)
                .EmailAddress();

            RuleFor(user => user.Password)
            .NotEmpty().WithMessage("A senha é obrigatória.");



        }
    }
}
