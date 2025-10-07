using CoordinateRegistration.Application.Dto.User;
using FluentValidation;

namespace CoordinateRegistration.Application.Validators.User
{
    public class UserDeleteValidator : AbstractValidator<UserDeleteDto>
    {
        public UserDeleteValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("A senha deve ser preenchida.");

            RuleFor(x => x.ConfirmedPassword)
                .NotEmpty()
                .WithMessage("A confirmção de senha deve ser preenchido.");


            RuleFor(user => user.ConfirmedPassword)
                .Equal(user => user.Password).WithMessage("As senhas devem ser iguais.");
        }
    }
}
