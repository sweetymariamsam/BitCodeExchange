using System;
using System.Net.Http;
using System.Threading.Tasks;

using BlockChainApp.Services.Interfaces;

namespace BlockChainApp.Services
{
    public class WebAccess: IWebAccess
    {
        private readonly HttpClient _httpClient;

        public WebAccess()
        {
            _httpClient = new HttpClient();
        }

        public WebAccess(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetAsyncRequest<T>(Uri restUri)
        {
            var response = await _httpClient.GetAsync(restUri);
            var result = await response?.Content?.ReadAsStringAsync();

            if (string.IsNullOrEmpty(result))
            {
                return null;
            }
            else
            {
                return result;
            }
        }
    }
}
