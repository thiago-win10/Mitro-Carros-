using BusinessInfo.Application.Common.Helpers;
using FluentValidation;

namespace BusinessInfo.Application.Vehicle.Command.Create
{
    public class CreateVehicleCommandValidator : AbstractValidator<CreateVehicleCommandRequest>
    {
        public CreateVehicleCommandValidator()
        {

            RuleFor(x => x.NameVehicle)
                .NotEmpty()
                .WithMessage("O campo Nome do Veiculo é obrigatório");

            RuleFor(x => x.Plate)
                .NotEmpty()
                .WithMessage("Campo placa é obrigatório")
                .Must(Validator.LicensePlateIsValid)
                .WithMessage("Placa inválida.");

            RuleFor(x => x.Year)
                .NotEmpty()
                .WithMessage("O campo Ano é obrigatório");

            RuleFor(x => x.Renavam)
              .NotEmpty()
              .WithMessage("O campo Renavam é obrigatório");

        }

    }
}
