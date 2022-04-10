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
        public string HttpClientName { get; private set; }

        public HttpClientIntegration(IAutranSoftFluentLogger<Integration> logger, System.Net.Http.IHttpClientFactory clientFactory, string httpClientName)
        {
            HttpClientName = httpClientName;
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public virtual System.Net.Http.HttpClient CreateHttpClient() => _clientFactory.CreateClient(HttpClientName);
        public RequestFluent<Integration> CreateFluentHttpClient() => CreateHttpClient().Fluent(_logger);
    }
}