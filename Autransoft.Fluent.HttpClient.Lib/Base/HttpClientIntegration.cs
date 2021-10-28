using Autransoft.Fluent.HttpClient.Lib.Extensions;
using Autransoft.Fluent.HttpClient.Lib.Fluents;
using Autransoft.Fluent.HttpClient.Lib.Interfaces;

namespace Autransoft.Fluent.HttpClient.Lib.Base
{
    public class HttpClientIntegration<Integration>
        where Integration : class
    {
        public readonly System.Net.Http.IHttpClientFactory _clientFactory;
        public readonly IAutranSoftFluentLogger<Integration> _logger;
        public readonly string _httpClientName;

        public HttpClientIntegration(IAutranSoftFluentLogger<Integration> logger, System.Net.Http.IHttpClientFactory clientFactory, string httpClientName)
        {
            _httpClientName = httpClientName;
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public virtual System.Net.Http.HttpClient CreateHttpClient() => _clientFactory.CreateClient(_httpClientName);
        public RequestFluent<Integration> CreateFluentHttpClient() => CreateHttpClient().Fluent(_logger);
    }
}