using BusinessInfo.Application.Common.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessInfo.Application.Issuer.Command.Create
{
    public class CreateIssuerCommandRequest : IRequest<ResponseApiBase<Guid>>
    {
        public Guid CompanyId { get; set; }

    }
}
