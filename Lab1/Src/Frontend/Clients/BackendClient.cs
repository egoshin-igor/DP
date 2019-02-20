using System.Net.Http;
using Lib.Http.Client;

namespace Frontend.Clients
{
    public class BackendClient: BaseClient
    {
        public BackendClient(HttpClient httpClient, GeneralSettings generalSettings)
            :base(httpClient, generalSettings.BackendBaseUrl)
        {            
        }
    }
}