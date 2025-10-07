using CoordinateRegistration.Application.Dto.Comment;
using FluentValidation;

namespace CoordinateRegistration.Application.Validators.Comment
{
    public class CommentAddValidator : AbstractValidator<CommentAddDto>
    {
        public CommentAddValidator() 
        {
            RuleFor(x => x.Text)
                .NotEmpty()
                .WithMessage("O comentário deve ser preenchido.")
                .MinimumLength(10)
                .MaximumLength(150);

            RuleFor(x => x.MarkerHash)
                .NotEmpty()
                .WithMessage("O marcador deve ser preenchido.");

        }
    }
}
