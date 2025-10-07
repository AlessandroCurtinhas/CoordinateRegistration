using CoordinateRegistration.Application.Dto.Review;
using FluentValidation;

namespace CoordinateRegistration.Application.Validators.Review
{
    public class ReviewAddValidator : AbstractValidator<ReviewAddDto>
    {
        public ReviewAddValidator() 
        {
            RuleFor(x => x.MarkerHash)
                .NotEmpty()
                .WithMessage("O marcador deve ser preenchido.");

            RuleFor(x => x.Positive)
                .NotEqual(x => x.Negative).WithMessage("As avaliações não podem ser iguais");
            
        }
    }
}
