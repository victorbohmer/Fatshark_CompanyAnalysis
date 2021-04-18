using Fatshark_CompanyAnalysis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Fatshark_CompanyAnalysis.API
{
    class ApiInterface
    {
        private readonly HttpClient client = new HttpClient();

        public async Task<List<PostcodeInfo>> GetPostcodeInfos(string[] postCodes)
        {
            var postcodeInfos = new List<PostcodeInfo>();
            var startIndex = 0;
            while (true)
            {
                var currentPostcodes = postCodes
                    .Skip(startIndex)
                    .Take(100)
                    .ToArray();
                postcodeInfos.AddRange(await GetUpTo100PostcodeInfos(currentPostcodes));
                
                startIndex += 100;
                if (startIndex > postCodes.Length)
                    break;
            }
            return postcodeInfos;
        }

        public async Task<List<PostcodeInfo>> GetUpTo100PostcodeInfos(string[] postCodes)
        {
            var requestData = new Dictionary<string, string[]>
            {
                { "postcodes", postCodes }
            };

            var content = JsonContent.Create(requestData, requestData.GetType());

            var response = await client.PostAsync("https://api.postcodes.io/postcodes?filter=postcode,eastings,northings,country", content);
            var responseString = await response.Content.ReadAsStringAsync();
            var responseDeserialized = JsonSerializer.Deserialize<Response>(responseString);

            return responseDeserialized.result.Select(r => r.result).Where(r => r != null).ToList();
        }

        public class Result
        {
            public string query { get; set; }
            public PostcodeInfo result { get; set; }
        }

        public class Response
        {
            public int status { get; set; }
            public IList<Result> result { get; set; }
        }
    }
}
