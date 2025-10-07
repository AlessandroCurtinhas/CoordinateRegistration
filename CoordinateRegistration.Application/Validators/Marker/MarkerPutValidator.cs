using CoordinateRegistration.Application.Dto.Marker;
using FluentValidation;

namespace CoordinateRegistration.Application.Validators.Marker
{
    public class MarkerPutValidator : AbstractValidator<MarkerPutDto>
    {
        public MarkerPutValidator()
        {

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("O comentário deve ser preenchido.")
                .MinimumLength(10)
                .MaximumLength(150);

            RuleFor(x => x.Lat)
                .NotEmpty()
                .WithMessage("A latitude deve ser preenchida.");

            RuleFor(x => x.Lng)
                .NotEmpty()
                .WithMessage("A longitude deve ser preenchida.");

            RuleFor(x => x.TypeOccurrenceHash)
                .NotEmpty()
                .WithMessage("O tipo de ocorrência deve ser preenchido.");

            RuleFor(x => x)
                .NotEmpty()
                .Must(ValidadorDuplicateTypeoccurrence)
                .WithMessage("A ocorrência deve ser informada apenas uma vez.");

        }


        private bool ValidadorDuplicateTypeoccurrence(MarkerPutDto request)
        {

            var duplicate = true;

            var countoccurrences = request.TypeOccurrenceHash.Count();
            var countDisctinct = request.TypeOccurrenceHash.Distinct().Count();
            if (countoccurrences > countDisctinct) duplicate = false;

            return duplicate;

        }
    }
}
