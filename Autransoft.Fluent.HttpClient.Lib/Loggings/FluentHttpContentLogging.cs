using Autransoft.Fluent.HttpClient.Lib.Exceptions;

namespace Autransoft.Fluent.HttpClient.Lib.Loggings
{
    public static class FluentHttpContentLogging
    {
        public static string LogInformation(this FluentHttpContentException ex)
        {
            var log = Logging.GetHeader(typeof(FluentHttpContentException));

            if(ex.Uri != null)
                log.Append($"Uri:{ex.Uri}|");

            return log.ToString().Substring(0, log.ToString().Length - 1);
        }

        public static string LogError(this FluentHttpContentException ex)
        {
            var log = Logging.GetHeader(typeof(FluentHttpContentException));

            if(ex.Uri != null)
                log.Append($"Uri:{ex.Uri}|");

            if(!string.IsNullOrEmpty(ex.Json))
                log.Append($"Json:{ex.Json}|");

            if(!string.IsNullOrEmpty(ex.PostmanCode))
                log.Append($"PostmanCode:{ex.PostmanCode}|");

            if(ex.HttpStatusCode != null)
                log.Append($"HttpStatusCode:{ex.HttpStatusCode.Value}|");

            if(!string.IsNullOrEmpty(ex.ContentResponse))
                log.Append($"Content:{ex.ContentResponse}|");

            Logging.GetExceptionMessage(log, ex);

            return log.ToString().Substring(0, log.ToString().Length - 1);
        }
    }
}