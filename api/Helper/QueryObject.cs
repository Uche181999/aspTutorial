using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helper
{
    public class QueryObject
    {
        public string? Symbol { get; set; } = null;
        public string? CompanyName { get; set; } = null ;
        public string?  OrderBy{ get; set; } = null ;
        public bool IsDescending { get; set; }=false;
        public int PageSize { get; set; } = 10;
        public int PageNum { get; set; } = 1;
    }
}