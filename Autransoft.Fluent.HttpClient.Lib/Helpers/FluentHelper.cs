using System;
using Autransoft.Fluent.HttpClient.Lib.Fluents;

namespace Autransoft.Fluent.HttpClient.Lib.Helpers
{
    public static class FluentHelper
    {
        public static Uri GetUri(this RequestFluent requestFluent, string urn) =>
            new Uri($"{requestFluent.HttpClient.BaseAddress.AbsoluteUri}/{urn}".Replace("//", "/").Replace("https:/", "https://").Replace("http:/", "http://"));
    }
}