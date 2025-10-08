using BusinessInfo.Application.Common.AES;
using BusinessInfo.Application.Common.Exceptions;
using BusinessInfo.Application.Common.Interfaces;
using BusinessInfo.Application.Common.Models.Response;
using BusinessInfo.Application.Company.Command.Create;
using BusinessInfo.Common;
using BusinessInfo.Domain.Entities;
using BusinessInfo.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

namespace BusinessInfo.Application.Company.Command.Create
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommandRequest, ResponseApiBase<Guid>>
    {
        private readonly IBusinessInfoContext _context;
        private readonly ILogger<CreateCompanyCommandHandler> _logger;
        private readonly AesEncryptionService _aesEncryptionService;


        public CreateCompanyCommandHandler(IBusinessInfoContext context, ILogger<CreateCompanyCommandHandler> logger, AesEncryptionService aesEncryptionService)
        {
            _context = context;
            _logger = logger;
            _aesEncryptionService = aesEncryptionService;
        }

        public async Task<ResponseApiBase<Guid>> Handle(CreateCompanyCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new ResponseApiBase<Guid>();

                _logger.LogInformation("Stating create Company {data}", request.ToJson());

                var company = await CompanyEntity(request, cancellationToken);
                response.AddSuccess(company.Id);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

        }

        private async Task<Domain.Entities.Company> CompanyEntity(CreateCompanyCommandRequest request, CancellationToken cancellationToken)
        {
            var emailCriptografado = _aesEncryptionService.Encrypt(request.ContactPerson.Email.Trim());

            Domain.Entities.Company? company = null;

            if (request.Id != Guid.Empty)
            {
                company = await _context.Companies
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (company == null)
                    throw new BadRequestException("Compania não encontrada para atualização.");

                var addressCompany = new Address(
                    request.Address.Street.Trim(),
                    request.Address.City.Trim(),
                    request.Address.Number.Trim(),
                    request.Address.Neighborhood.Trim(),
                    request.Address.State.Trim(),
                    request.Address.ZipCode.Trim());

                var contactPerson = new Person(
                    request.ContactPerson.FullName.Trim(),
                    request.ContactPerson.Occupation.Trim(),
                    emailCriptografado,
                    request.ContactPerson.Phone.Trim().UnMask()
                );

                company.UpdateData(addressCompany, contactPerson, request.Name, request.Cnpj.Trim().UnMask(), request.Segment);

                _context.Companies.Update(company);
            }
            else
            {
                var existingCompany = await _context.Companies
                    .AnyAsync(x => x.ContactPerson.Email == emailCriptografado, cancellationToken);

                if (existingCompany)
                    throw new BadRequestException("Já existe uma Compania com este email.");

                var addressCompany = new Address(
                    request.Address.Street.Trim(),
                    request.Address.City.Trim(),
                    request.Address.Number.Trim(),
                    request.Address.Neighborhood.Trim(),
                    request.Address.State.Trim(),
                    request.Address.ZipCode.Trim());

                var contactPerson = new Person(
                    request.ContactPerson.FullName.Trim(),
                    request.ContactPerson.Occupation.Trim(),
                    emailCriptografado,
                    request.ContactPerson.Phone.Trim().UnMask()
                );

                company = new Domain.Entities.Company(addressCompany, contactPerson, request.Name, request.Cnpj, request.Segment);

                await _context.Companies.AddAsync(company, cancellationToken);
            }

            await _context.SaveChangesAsync(cancellationToken);
            return company;
        }


        //private async Task<Domain.Entities.Company> CompanyEntity(CreateCompanyCommandRequest request, CancellationToken cancellationToken)
        //{
        //    var cripto = _aesEncryptionService.Encrypt(request.ContactPerson.Email);
        //    var existingCompany = await _context.Companies.AnyAsync(x => x.ContactPerson.Email == cripto, cancellationToken);
        //    if (existingCompany)
        //        throw new BadRequestException("Já existe uma Compania com este email.");

        //    var addressCompany = new Address(
        //        request.Address.Street.Trim(),
        //        request.Address.City.Trim(),
        //        request.Address.Number.Trim(),
        //        request.Address.Neighborhood.Trim(),
        //        request.Address.State.Trim(),
        //        request.Address.ZipCode.Trim());

        //    var contactPerson = new Person(
        //        request.ContactPerson.FullName.Trim(),
        //        _aesEncryptionService.Encrypt(request.ContactPerson.Email.Trim()),
        //        request.ContactPerson.Phone.Trim().UnMask(),
        //        request.ContactPerson.Occupation.Trim()
        //        );

        //    var company = new Domain.Entities.Company(addressCompany, contactPerson, request.Name, request.Cnpj, request.Segment);

        //    await _context.Companies.AddAsync(company);

        //    await _context.SaveChangesAsync(cancellationToken);

        //    return company;
        //}
    }
}



