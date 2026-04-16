using CoordinateRegistration.Application.Dto.Authentication;
using FluentValidation;

namespace CoordinateRegistration.Application.Validators.Authentication
{
    public class PersonRecoveryPasswordValidator : AbstractValidator<PersonRecoveryPasswordDto>
    {
        public PersonRecoveryPasswordValidator()
        {

            RuleFor(person => person.Password)
            .NotEmpty().WithMessage("A senha é obrigatória.")
            .MinimumLength(8).WithMessage("A senha deve ter pelo menos 8 caracteres.")
            .Matches("[A-Z]").WithMessage("A senha deve conter pelo menos uma letra maiúscula.")
            .Matches("[a-z]").WithMessage("A senha deve conter pelo menos uma letra minúscula.")
            .Matches("[0-9]").WithMessage("A senha deve conter pelo menos um número.")
            .Matches("[^a-zA-Z0-9]").WithMessage("A senha deve conter pelo menos um caractere especial.");


            RuleFor(person => person.ConfirmedPassword)
            .NotEmpty().WithMessage("A confirmação de senha é obrigatória.")
            .Equal(person => person.Password).WithMessage("As senhas devem ser iguais.");

        }
    }
}
