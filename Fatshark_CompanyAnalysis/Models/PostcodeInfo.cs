using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fatshark_CompanyAnalysis.Models
{
    public class PostcodeInfo
    {
        public int PostcodeInfoId { get; set; }
        public string postcode { get; set; }
        public int eastings { get; set; }
        public int northings { get; set; }
        public string country { get; set; }
    }
}
