using CoordinateRegistration.Application.Dto.Authentication;
using FluentValidation;

namespace CoordinateRegistration.Application.Validators.Authentication
{
    public class PersonLoginValidator : AbstractValidator<PersonLoginDto>
    {

        public PersonLoginValidator()
        {

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("O email deve ser preenchido.")
                .MinimumLength(3)
                .MaximumLength(150)
                .EmailAddress().WithMessage("O email informado é inválido.");

            RuleFor(user => user.Password)
            .NotEmpty().WithMessage("A senha deve ser preenchida.");

            RuleFor(x => x)
                .NotNull().WithMessage("A teste de mensagem");



        }
    }
}
