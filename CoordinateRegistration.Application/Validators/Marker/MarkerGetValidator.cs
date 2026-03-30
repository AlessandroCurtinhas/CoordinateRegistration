using CoordinateRegistration.Application.Dto.Marker;
using FluentValidation;

namespace CoordinateRegistration.Application.Validators.Marker
{
    public class MarkerGetValidator: AbstractValidator<MarkerGetDto>
    {
        public MarkerGetValidator()
        {
            
            RuleFor(x => x.dateStart)
                .NotEmpty().WithMessage("A data de inicio deve ser preenchida.")
                .Must(BeAValidDate).WithMessage("Formato de data inválido.");

            RuleFor(x => x.dateFinal)
                .NotEmpty().WithMessage("A data final deve ser preenchida.")
                .Must(BeAValidDate).WithMessage("Formato de data inválido.");


        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }


    }
}
