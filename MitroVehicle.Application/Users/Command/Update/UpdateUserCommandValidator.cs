using FluentValidation;
using MitroVehicle.Application.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MitroVehicle.Application.Users.Command.Update
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommandRequest>
    {
        public UpdateUserCommandValidator()
        {
           
            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage("O campo Telefone é obrigatório")
                .NotNull()
                .WithMessage("O campo Telefone é obrigatório")
                .Must(Validator.IsValidPhoneNumber)
                .WithMessage("Telefone inválido.");

            RuleFor(x => x.Age)
              .NotEmpty()
              .WithMessage("O campo idade é obrigatório")
              .NotNull()
              .WithMessage("O campo idade é obrigatório");

            RuleFor(x => x.Email)
              .NotEmpty()
              .WithMessage("O campo email é obrigatório")
              .NotNull()
              .WithMessage("O campo email é obrigatório")
              .Must(Validator.IsValidEmail)
              .WithMessage("Email inválido");

            RuleFor(x => x.Password)
              .NotEmpty()
              .WithMessage("O campo senha é obrigatório")
              .NotNull()
              .WithMessage("O campo senha é obrigatório");

        }
    }
}
