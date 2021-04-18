using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fatshark_CompanyAnalysis.Models
{
    public class CompanySet
    {
        public int CompanySetId { get; set; }
        public string Name { get; set; }
        public List<Company> Companies { get; set; }

    }
}
