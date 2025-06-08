using FluentValidation;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MitroVehicle.Application.Common.Helpers;

namespace MitroVehicle.Application.Users.Command.Register
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommandRequest>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("O campo Nome é obrigatório");

            RuleFor(x => x.Document)
                .NotEmpty()
                .WithMessage("O campo CPF/CNPJ é obrigatório")
                .NotNull()
                .Must(Validator.IsValidDocument)
                .WithMessage("Documento inválido. Informe um CPF ou CNPJ válido.");

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