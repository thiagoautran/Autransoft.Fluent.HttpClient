using Autransoft.Fluent.HttpClient.Lib.Fluents;
using System;

namespace Autransoft.Fluent.HttpClient.Lib.Helpers
{
    public static class FluentHelper
    {
        public static Uri GetUri<Integration>(this RequestFluent<Integration> requestFluent, string urn)
            where Integration : class =>
            new Uri($"{requestFluent.HttpClient.BaseAddress.AbsoluteUri}/{urn}".Replace("//", "/").Replace("https:/", "https://").Replace("http:/", "http://"));
    }
}