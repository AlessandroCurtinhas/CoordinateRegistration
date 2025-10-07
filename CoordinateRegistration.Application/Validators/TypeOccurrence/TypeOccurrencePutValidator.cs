using CoordinateRegistration.Application.Dto.TypeOccurrence;
using FluentValidation;

namespace CoordinateRegistration.Application.Validators.TypeOccurrence
{
    public class TypeOccurrencePutValidator : AbstractValidator<TypeOccurrencePutDto>
    {
        public TypeOccurrencePutValidator()
        {

            RuleFor(x => x.Hash)
                .NotEmpty()
                .WithMessage("O hash deve ser preenchido.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("O nome deve ser preenchido.")
                .MinimumLength(10)
                .MaximumLength(50);

        }

    }
}
