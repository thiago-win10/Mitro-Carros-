using BusinessInfo.Application.Common.Models.Response;
using BusinessInfo.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessInfo.Application.Company.Command.Create
{
    public class CreateCompanyCommandRequest : IRequest<ResponseApiBase<Guid>>
    {
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public string Segment { get; set; }
        public Address Address { get; set; }
        public Person ContactPerson { get; set; }
    }
}
