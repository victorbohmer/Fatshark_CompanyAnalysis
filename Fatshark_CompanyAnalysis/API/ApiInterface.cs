using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
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

            var response = await client.PostAsync("https://api.postcodes.io/postcodes", content);

            var responseString = await response.Content.ReadAsStringAsync();

        }
    }
}
