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
        DataContext context;
        MainWindow mainWindow;
        public DataHandler(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            context = new DataContext();
            context.Database.EnsureCreated();
        }
        public void CreateCompanySetFromIncludedSampleFile()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, @"Files\", "uk-500.csv");
            CreateCompanySetFromFile(filePath);
        }
        public void CreateCompanySetFromFile(string filePath)
        {
            var companies = ReadCompaniesFromFile(filePath);

            mainWindow.AddLogEntry($"Successfully parsed {companies.Count()} companies from file");

            var fileName = filePath.Split("\\").Last();
            var companySetName = $"{fileName} - {DateTime.Now}";

            mainWindow.AddLogEntry($"Successfully saved new CompanySet {companySetName}");

            SaveCompanySetToDatabase(companies, companySetName);
        }

        private void SaveCompanySetToDatabase(List<Company> companies, string companySetName)
        {
            context.CompanySets.Add(new CompanySet() { Companies = companies, Name = companySetName });
            context.SaveChanges();
        }

        public List<Company> ReadCompaniesFromFile(string filePath = null)
        {
            var csvLines = File.ReadAllLines(filePath).Skip(1);

            return csvLines.Select(x => Company.FromCsv(x)).ToList();
        }
    }
}
