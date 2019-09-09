using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagement.DTOs
{
    public class FilterDTO
    {
        public int? Page { get; set; }
        public int? Limit { get; set; }
        public int? TotalItems { get; set; }
        public string Search { get; set; }
        public string Sort { get; set; }
        public bool Desc { get; set; }
    }
}
