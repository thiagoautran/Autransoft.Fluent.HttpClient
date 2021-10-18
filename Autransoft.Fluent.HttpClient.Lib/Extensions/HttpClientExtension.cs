using Autransoft.Fluent.HttpClient.Lib.Fluents;

namespace Autransoft.Fluent.HttpClient.Lib.Extensions
{
    public static class HttpClientExtension
    {
        public static RequestFluent Fluent(this System.Net.Http.HttpClient httpClient) => new RequestFluent(httpClient);
    }
}