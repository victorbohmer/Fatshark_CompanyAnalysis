using Fatshark_CompanyAnalysis.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fatshark_CompanyAnalysis.Data
{
    public class DataHandler
    {
        public void CreateCompanySetFromFile(string filePath = null)
        {
            if (filePath == null)
                filePath = Path.Combine(Environment.CurrentDirectory, @"Files\", "uk-500.csv");

            var companies = ReadCompaniesFromFile(filePath);

        }
        public List<Company> ReadCompaniesFromFile(string filePath = null)
        {
            var csvLines = File.ReadAllLines(filePath).Skip(1);

            return csvLines.Select(x => Company.FromCsv(x)).ToList();
        }
    }
}
