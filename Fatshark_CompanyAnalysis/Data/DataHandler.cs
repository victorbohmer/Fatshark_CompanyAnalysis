using Fatshark_CompanyAnalysis.API;
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
        ApiInterface apiInterface;
        public int CompanySetId { get { return mainWindow.CompanySet.CompanySetId; } }
        public DataHandler(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            context = new DataContext();
            context.Database.EnsureCreated();
            apiInterface = new ApiInterface();
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

        internal Dictionary<string, string> GetPopularDomains()
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
                .ToDictionary(g => g.Key, g => g.Count().ToString());

            return popularDomains;
        }

        internal async Task<Dictionary<string, string>> GetCompanyClusters(int scaleInMeters)
        {
            await EnsurePostcodeInfoExists();

            return GetCompanyClustersFromDatabase(scaleInMeters);
        }
        internal async Task<Dictionary<string, string>> GetCountryDistribution()
        {
            await EnsurePostcodeInfoExists();

            return GetCountryDistributionFromDatabase();
        }

        

        private async Task EnsurePostcodeInfoExists()
        {
            var postCodesMissingInfo = GetPostcodesMissingInfo();

            if (postCodesMissingInfo.Length > 0)
            {
                mainWindow.AddLogEntry($"Missing info for {postCodesMissingInfo.Count()} postcodes");
                await GetPostcodeInfos(postCodesMissingInfo);
            }
        }

        private string[] GetPostcodesMissingInfo()
        {
            var postCodesMissingInfo = context.Companies
                .Where(c => c.CompanySetId == CompanySetId && !context.PostcodeInfos.Select(p => p.postcode).Contains(c.Postal))
                .Select(c => c.Postal)
                .Distinct()
                .ToArray();

            return postCodesMissingInfo;
        }

        private async Task GetPostcodeInfos(string[] postCodesMissingInfo)
        {
            var postcodeInfo = await apiInterface.GetPostcodeInfos(postCodesMissingInfo);

            mainWindow.AddLogEntry($"Successfully fetched info for {postcodeInfo.Count()} postcodes");
            if (postcodeInfo.Count() < postCodesMissingInfo.Length)
                mainWindow.AddLogEntry($"Failed to fetch info for {postCodesMissingInfo.Length - postcodeInfo.Count()} postcodes");

            await context.AddRangeAsync(postcodeInfo);
            await context.SaveChangesAsync();
        }

        private Dictionary<string, string> GetCompanyClustersFromDatabase(int scaleInMeters = 20000, int minCount = 5, int maxResults = 20)
        {
            var roundedCoordinates = context.Companies
                .Join(context.PostcodeInfos, c => c.Postal, p => p.postcode, (c, p) =>
                new { RoundedNorthings = (p.northings / scaleInMeters) * scaleInMeters, RoundedEastings = (p.eastings / scaleInMeters) * scaleInMeters })
                .ToArray();

            var clusters = roundedCoordinates
                .GroupBy(x => $"Northings: {x.RoundedNorthings}, Eastings: {x.RoundedEastings}")
                .Where(g => g.Count() >= minCount)
                .OrderByDescending(g => g.Count())
                .Take(maxResults)
                .ToDictionary(g => g.Key, g => g.Count().ToString());

            return clusters;
        }

        private Dictionary<string, string> GetCountryDistributionFromDatabase()
        {
            var countries = context.Companies
                .Join(context.PostcodeInfos, c => c.Postal, p => p.postcode, (c, p) => p.country)
                .ToArray();

            var countryDistribution = countries
                .GroupBy(x => x)
                .ToDictionary(g => g.Key, g => $"{g.Count() * 100 / countries.Length}%");

            return countryDistribution;
        }

    }
}
