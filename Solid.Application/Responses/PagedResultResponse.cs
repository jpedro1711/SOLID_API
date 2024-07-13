using SOLID.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Application.Responses
{
    public class PagedResultResponse<T> where T : class 
    {
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 0;
        public int TotalPages { get; set; } = 0;
        public int TotalItems { get; set; } = 0;    
        public IEnumerable<T> data { get; set; } = Enumerable.Empty<T>();
    }
}
