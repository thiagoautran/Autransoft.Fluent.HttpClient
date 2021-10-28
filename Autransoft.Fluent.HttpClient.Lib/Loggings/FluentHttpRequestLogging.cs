using Autransoft.Fluent.HttpClient.Lib.Exceptions;
using Autransoft.Fluent.HttpClient.Lib.Fluents;

namespace Autransoft.Fluent.HttpClient.Lib.Loggings
{
    internal static class FluentHttpRequestLogging
    {
        public static string Error<Integration>(this FluentHttpRequestException<Integration> ex)
            where Integration : class
        {
            var log = Logging.GetErrorHeader(typeof(FluentHttpRequestException<Integration>), typeof(Integration));

            if(ex.Uri != null)
                log.Append($"Uri:{ex.Uri}|");

            if(!string.IsNullOrEmpty(ex.Json))
                log.Append($"Json:{ex.Json}|");

            if(!string.IsNullOrEmpty(ex.PostmanCode))
                log.Append($"PostmanCode:{ex.PostmanCode}|");

            if(ex.HttpStatusCode != null)
                log.Append($"HttpStatusCode:{ex.HttpStatusCode.Value}|");

            Logging.GetExceptionMessage(log, ex);

            return log.ToString().Substring(0, log.ToString().Length - 1);
        }
    }
}