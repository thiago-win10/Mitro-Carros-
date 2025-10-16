using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessInfo.Application.Common.Models.Response
{
    public class PaginatedModelResponse<T>
    {
        public int Total { get; set; }
        public IEnumerable<T> Items { get; set; }
        protected PaginatedModelResponse() { }
        public PaginatedModelResponse(int total, IEnumerable<T> items)
        {
            Total = total;
            Items = items;
        }
    }
}
