using CoordinateRegistration.Application.Dto.Authentication;
using FluentValidation;

namespace CoordinateRegistration.Application.Validators.Authentication
{
    public class UserRecoveryPasswordValidator : AbstractValidator<UserRecoveryPasswordDto>
    {
        public UserRecoveryPasswordValidator()
        {

            RuleFor(user => user.Password)
            .NotEmpty().WithMessage("A senha é obrigatória.")
            .MinimumLength(8).WithMessage("A senha deve ter pelo menos 8 caracteres.")
            .Matches("[A-Z]").WithMessage("A senha deve conter pelo menos uma letra maiúscula.")
            .Matches("[a-z]").WithMessage("A senha deve conter pelo menos uma letra minúscula.")
            .Matches("[0-9]").WithMessage("A senha deve conter pelo menos um número.")
            .Matches("[^a-zA-Z0-9]").WithMessage("A senha deve conter pelo menos um caractere especial.");

            RuleFor(user => user.ConfirmedPassword)
            .Equal(user => user.Password).WithMessage("As senhas devem ser iguais.");

        }
    }
}
