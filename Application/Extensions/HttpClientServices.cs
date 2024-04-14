using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
namespace anskus.Application.Extensions
{
    public  class HttpClientServices(IHttpClientFactory httpClientFactory,LocalStorageServices localStorageServices)
    {
        private HttpClient CreateClient()=>httpClientFactory!.CreateClient(Constant.HttpClientName);
        public HttpClient GetPublicClient()=>CreateClient();
        public async Task<HttpClient> GetPrivateClient()
        {
            try
            {
                var client = CreateClient();
                var localStorageDTo = await localStorageServices.GetModelFromToken();
                if(string.IsNullOrEmpty(localStorageDTo.Token))
                {
                    return client;
                }
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constant.HttpClientHadersSchame, localStorageDTo.Token);
                return client;
            }
            catch (Exception ex)
            {
                return new HttpClient();
            }
        }
    }
}
