using CoordinateRegistration.Application.Dto.User;
using FluentValidation;

namespace CoordinateRegistration.Application.Validators.User
{
    public class UserPutValidator : AbstractValidator<UserPutDto>
    {
        public UserPutValidator()
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
