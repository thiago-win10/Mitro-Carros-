using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessInfo.Application.Issuer.Queries
{
    public class ListIssuerQueryResponse
    {
        public Guid IssuerId {  get; set; }
        public string NameIssuer { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
