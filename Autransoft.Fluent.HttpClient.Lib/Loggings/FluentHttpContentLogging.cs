using Autransoft.Fluent.HttpClient.Lib.Exceptions;
using Autransoft.Fluent.HttpClient.Lib.Fluents;

namespace Autransoft.Fluent.HttpClient.Lib.Loggings
{
    internal static class FluentHttpContentLogging
    {
        public static string LogInformation(this ResponseFluent responseFluent)
        {
            var log = Logging.GetInformationHeader();

            if(responseFluent.Request.Uri != null)
                log.Append($"Uri:{responseFluent.Request.Uri}|");

            return log.ToString().Substring(0, log.ToString().Length - 1);
        }

        public static string LogError(this FluentHttpContentException ex)
        {
            var log = Logging.GetErrorHeader(typeof(FluentHttpContentException));

            if(ex.Uri != null)
                log.Append($"Uri:{ex.Uri}|");

            if(!string.IsNullOrEmpty(ex.Json))
                log.Append($"Json:{ex.Json}|");

            if(ex.HttpStatusCode != null)
                log.Append($"HttpStatusCode:{ex.HttpStatusCode.Value}|");

            if(!string.IsNullOrEmpty(ex.PostmanCode))
                log.Append($"PostmanCode:{ex.PostmanCode}|");

            if(!string.IsNullOrEmpty(ex.ContentResponse))
                log.Append($"Content:{ex.ContentResponse}|");

            Logging.GetExceptionMessage(log, ex);

            return log.ToString().Substring(0, log.ToString().Length - 1);
        }
    }
}