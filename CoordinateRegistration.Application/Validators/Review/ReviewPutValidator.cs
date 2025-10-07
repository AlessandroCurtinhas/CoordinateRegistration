using CoordinateRegistration.Application.Dto.Review;
using FluentValidation;

namespace CoordinateRegistration.Application.Validators.Review
{
    public class ReviewPutValidator : AbstractValidator<ReviewPutDto>
    {
        public ReviewPutValidator()
        {
            RuleFor(x => x.Hash)
                .NotEmpty()
                .WithMessage("O hash da avaliação deve ser preenchido.");

            RuleFor(x => x.Positive)
                .NotEqual(x => x.Negative).WithMessage("As avaliações não podem ser iguais");

        }
    }
}
