//using FluentValidation;
//using MitroVehicle.Application.Common.Helpers;
//using MitroVehicle.Domain.Enumerators;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MitroVehicle.Application.VehicleSaved.Command.Create
//{
//    public class CreateVehicleCommandValidator : AbstractValidator<CreateVehicleCommandRequest>
//    {
//        public CreateVehicleCommandValidator()
//        {

//            RuleFor(x => x.NameVehicle)
//                .NotEmpty()
//                .WithMessage("O campo Nome do Veiculo é obrigatório");

//            RuleFor(x => x.LicensePlate)
//                .NotEmpty()
//                .WithMessage("Campo placa é obrigatório")
//                .Must(Validator.LicensePlateIsValid)
//                .WithMessage("Placa inválida.");

//            RuleFor(x => x.Year)
//                .NotEmpty()
//                .WithMessage("O campo Ano é obrigatório");

//            RuleFor(x => x.UF)
//              .NotEmpty()
//              .WithMessage("O campo UF é obrigatório")
//              .Must(Validator.IsValidUf)
//              .WithMessage("UF inválido.");

//            RuleFor(x => x.Renavam)
//              .NotEmpty()
//              .WithMessage("O campo Renavam é obrigatório");

//            RuleFor(x => x.Color)
//              .NotEmpty()
//              .WithMessage("O campo Cor é obrigatório");

//        }

//    }
//}
