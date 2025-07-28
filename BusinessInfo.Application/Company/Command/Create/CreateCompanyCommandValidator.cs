using BusinessInfo.Application.Common.Helpers;
using BusinessInfo.Domain.ValueObjects;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessInfo.Application.Company.Command.Create
{
    public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommandRequest>
    {
        public CreateCompanyCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("O campo Nome é obrigatório");

            RuleFor(x => x.Cnpj)
                .NotEmpty()
                .WithMessage("O campo CNPJ é obrigatório")
                .NotNull()
                .Must(Validator.CNPJIsValid)
                .WithMessage("Documento inválido. Informe um CNPJ válido.");

            RuleFor(x => x.Segment)
                .NotEmpty()
                .WithMessage("Campo obrigatório");

            RuleFor(x => x.ContactPerson.Phone)
                .NotEmpty()
                .WithMessage("O campo Telefone é obrigatório")
                .NotNull()
                .WithMessage("O campo Telefone é obrigatório")
                .Must(Validator.IsValidPhoneNumber)
                .WithMessage("Telefone inválido.");

            RuleFor(x => x.ContactPerson.Email)
              .NotEmpty()
              .WithMessage("O campo email é obrigatório")
              .NotNull()
              .WithMessage("O campo email é obrigatório")
              .Must(Validator.IsValidEmail)
              .WithMessage("Email inválido");

            RuleFor(x => x.Address.ZipCode)
              .NotEmpty()
              .WithMessage("O campo Cep é obrigatório")
              .NotNull();

            RuleFor(x => x.Address.State)
              .NotEmpty()
              .WithMessage("O Estado é obrigatório")
              .NotNull();

            RuleFor(x => x.Address.Street)
              .NotEmpty()
              .WithMessage("O nome da Rua/Avenida é obrigatório")
              .NotNull();


            RuleFor(x => x.Address.Number)
              .NotEmpty()
              .WithMessage("O numero do endereço é obrigatório")
              .NotNull();
        }
    }
}

