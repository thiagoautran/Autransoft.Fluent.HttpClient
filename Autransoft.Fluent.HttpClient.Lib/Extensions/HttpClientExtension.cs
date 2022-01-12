using Autransoft.Fluent.HttpClient.Lib.Fluents;
using Autransoft.Fluent.HttpClient.Lib.Interfaces;

namespace Autransoft.Fluent.HttpClient.Lib.Extensions
{
    public static class HttpClientExtension
    {
        public static RequestFluent<Integration> Fluent<Integration>(this System.Net.Http.HttpClient httpClient, IAutranSoftFluentLogger<Integration> logger)
            where Integration : class => 
            new RequestFluent<Integration>(httpClient, logger);

        public static RequestFluentTest Fluent(this System.Net.Http.HttpClient httpClient) =>
            new RequestFluentTest(httpClient);
    }
}