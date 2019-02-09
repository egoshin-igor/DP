using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Frontend
{
    public class BackendClient
    {
        private static HttpClient _client = new HttpClient();

        public BackendClient(GeneralSettings generalSettings) {
            _client.BaseAddress = new Uri(generalSettings.BackendBaseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<R> PostAsync<R, D>(D data, string path)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync(path, data);

            return await response.Content.ReadAsAsync<R>();
        }
    }
}