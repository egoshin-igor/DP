using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Lib.Http.Client
{
    public class BaseClient
    {
        private readonly HttpClient _client;

        protected BaseClient(HttpClient client, string baseUrl)
        {
            _client = client;
            _client.BaseAddress = new Uri(baseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private BaseClient()
        {
        }

        public async Task<Response<R>>   PostAsync<R, D>(D data, string path)
        {
            const int internalServerError = 500;

            var result = new Response<R>();

            HttpResponseMessage httpResponse = null;
            try
            {
                httpResponse = await _client.PostAsJsonAsync(path, data);
                if (httpResponse.IsSuccessStatusCode)
                {
                    result.Result = await httpResponse.Content.ReadAsAsync<R>();
                }
                result.StatusCode = (int)httpResponse.StatusCode;
            }
            catch(HttpRequestException)
            {
                result.StatusCode = internalServerError;
            }
            finally
            {
                if (httpResponse != null)
                {
                    httpResponse.Dispose();
                }
            }
            

            return result;
        }
    }
}