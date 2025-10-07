using CoordinateRegistration.Application.Dto.TypeOccurrence;
using FluentValidation;

namespace CoordinateRegistration.Application.Validators.TypeOccurrence
{
    public class TypeOccurrenceAddValidator : AbstractValidator<TypeOccurrenceAddDto>
    {
        public TypeOccurrenceAddValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("O nome deve ser preenchido.")
                .MinimumLength(3)
                .MaximumLength(50);

        }

    }
}
