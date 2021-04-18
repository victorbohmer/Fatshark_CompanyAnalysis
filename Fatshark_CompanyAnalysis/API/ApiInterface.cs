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

        public async Task GetPostcodeInfo()
        {
            await GetPostcodeInfo(new string[] { "CT2 7PP", "DA2 7PP" });
        }
        public async Task GetPostcodeInfo(string[] postCodes)
        {
            var requestData = new Dictionary<string, string[]>
            {
                { "postcodes", postCodes }
            };

            var content = JsonContent.Create(requestData, requestData.GetType());

            var response = await client.PostAsync("https://api.postcodes.io/postcodes?filter=postcode,eastings,northings", content);

            var responseString = await response.Content.ReadAsStringAsync();

            var responseDeserialized = JsonSerializer.Deserialize<Response>(responseString);

            var postCodeInfos = responseDeserialized.result.Select(r => r.result).ToArray();

        }


        public class PostCodeInfo
        {
            public string postcode { get; set; }
            public int eastings { get; set; }
            public int northings { get; set; }
        }

        public class Result
        {
            public string query { get; set; }
            public PostCodeInfo result { get; set; }
        }

        public class Response
        {
            public int status { get; set; }
            public IList<Result> result { get; set; }
        }
    }
}
