using System.Net.Http;

namespace Iubh.RadioApp.Core.Clients
{
    public class NoCacheHttpClient: HttpClient
    {
        public NoCacheHttpClient()
        {
            this.DefaultRequestHeaders.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue
            {
                NoCache = true
            };
        }
    }
}
