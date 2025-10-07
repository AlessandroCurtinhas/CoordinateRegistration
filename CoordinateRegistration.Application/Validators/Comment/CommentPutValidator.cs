using CoordinateRegistration.Application.Dto.Comment;
using FluentValidation;

namespace CoordinateRegistration.Application.Validators.Comment
{
    public class CommentPutValidator : AbstractValidator<CommentPutDto>
    {
        public CommentPutValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty()
                .WithMessage("O comentário deve ser preenchido.")
                .MinimumLength(10)
                .MaximumLength(150);

            RuleFor(x => x.MarkerHash)
                .NotEmpty()
                .WithMessage("O maker deve ser preenchido.");

            RuleFor(x => x.MarkerHash)
                .NotEmpty()
                .WithMessage("O marcador deve ser preenchido.");

        }
    }
}
