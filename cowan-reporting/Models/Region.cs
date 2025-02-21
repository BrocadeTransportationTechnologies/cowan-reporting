using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsReporting.Models
{
    public class Region
    {
        public string? RegionName { get; set; }
        public List<string>? Terminals { get; set; }
        public string? AccountingCodes { get; set; }
    }
}
