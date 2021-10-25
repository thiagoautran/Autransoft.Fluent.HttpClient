namespace Autransoft.Fluent.HttpClient.Lib.Base
{
    public class HttpClientIntegration
    {
        public readonly System.Net.Http.IHttpClientFactory _clientFactory;
        public readonly string _httpClientName;

        public HttpClientIntegration(System.Net.Http.IHttpClientFactory clientFactory, string httpClientName)
        {
            _clientFactory = clientFactory;
            _httpClientName = httpClientName;
        }

        public virtual System.Net.Http.HttpClient CreateHttpClient()
        {
            return _clientFactory.CreateClient(_httpClientName);
        }
    }
}