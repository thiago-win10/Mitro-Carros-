using BusinessInfo.Application.Common.AES;
using BusinessInfo.Application.Common.Exceptions;
using BusinessInfo.Application.Common.Interfaces;
using BusinessInfo.Application.Common.Models.Response;
using BusinessInfo.Common;
using BusinessInfo.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
            var cripto = _aesEncryptionService.Encrypt(request.ContactPerson.Email);
            var existingCompany = await _context.Companies.AnyAsync(x => x.ContactPerson.Email == cripto, cancellationToken);
            if (existingCompany)
                throw new BadRequestException("Já existe uma Compania com este email.");

            var company = new Domain.Entities.Company
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Cnpj = request.Cnpj,
                Segment = request.Segment,
                ContactPerson = new Domain.ValueObjects.Person
                {
                    FullName = request.ContactPerson.FullName.Trim(),
                    Email = _aesEncryptionService.Encrypt(request.ContactPerson.Email.Trim()),
                    Phone = request.ContactPerson.Phone.Trim().UnMask(),
                    Occupation = request.ContactPerson.Occupation.Trim()
                },
                Address = new Domain.ValueObjects.Address
                {
                    Street = request.Address.Street.Trim(),
                    City = request.Address.City.Trim(),
                    Number = request.Address.Number.Trim(),
                    Neighborhood = request.Address.Neighborhood.Trim(),
                    State  = request.Address.State.Trim(),
                    ZipCode = request.Address.ZipCode.Trim(),
                }
            };
            await _context.Companies.AddAsync(company);

            await _context.SaveChangesAsync(cancellationToken);

            return company;
        }
    }
}
