using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Filters
{
    public abstract class PaginationFilter : Filters
    {
        protected PaginationFilter()
        {
            PageSize = int.MaxValue;
        }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string[] OrderBy { get; set; }
    }
}
