﻿using Fatshark_CompanyAnalysis.Models;
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
        public int CompanySetId { get { return mainWindow.CompanySet.CompanySetId; } }
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

        internal CompanySet GetFirstCompanySet()
        {
            var firstCompanySet = context.CompanySets.FirstOrDefault();
            if (firstCompanySet == null)
            {
                CreateCompanySetFromIncludedSampleFile();
                firstCompanySet = context.CompanySets.FirstOrDefault();
            }
            return firstCompanySet;
        }

        public List<Company> ReadCompaniesFromFile(string filePath = null)
        {
            var csvLines = File.ReadAllLines(filePath).Skip(1);

            return csvLines.Select(x => Company.FromCsv(x)).ToList();
        }

        public List<CompanySet> GetCompanySets()
        {
            return context.CompanySets.ToList();
        }

        internal Dictionary<string, int> GetPopularDomains()
        {
            var minCountToBeIncluded = 5;

            var emailAdresses = context.Companies
                .Where(c => c.CompanySetId == CompanySetId)
                .Select(c => c.Email)
                .ToArray();

            var popularDomains = emailAdresses
                .GroupBy(c => c.Substring(c.IndexOf('@')))
                .Where(g => g.Count() >= minCountToBeIncluded)
                .OrderByDescending(g => g.Count())
                .ToDictionary(g => g.Key, g => g.Count());

            return popularDomains;
        }
    }
}
