using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fatshark_CompanyAnalysis.Models
{
    class Company
    {
        public int CompanyId { get; set; }
        public int PostCodeInfoId { get; set; }
        public int DatasetId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Postal { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }

        public static Company FromCsv(string csvLine)
        {
            string[] values = csvLine.Trim('"').Split("\",\"");
            var expectedDatapoints = 11;
            if (values.Count() != expectedDatapoints)
                throw new FormatException($"Csv line split into {values.Count()} data points, expected {expectedDatapoints}");

            Company company = new Company();
            company.FirstName = values[0];
            company.LastName = values[1];
            company.CompanyName = values[2];
            company.Address = values[3];
            company.City = values[4];
            company.County = values[5];
            company.Postal = values[6];
            company.Phone1 = values[7];
            company.Phone2 = values[8];
            company.Email = values[9];
            company.Web = values[10];

            return company;
        }
    }
}
