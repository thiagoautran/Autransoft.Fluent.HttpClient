using Autransoft.Fluent.HttpClient.Lib.Exceptions;

namespace Autransoft.Fluent.HttpClient.Lib.Loggings
{
    internal static class FluentHttpContentLogging
    {
        public static string Info<Integration>(this FluentHttpContentException<Integration> ex)
            where Integration : class
        {
            var log = Logging.GetInfoHeader(typeof(FluentHttpContentException<Integration>), typeof(Integration));

            if(!string.IsNullOrEmpty(ex.Verb))
                log.Append($"Verb:{ex.Verb}|");

            if(ex.Uri != null)
                log.Append($"Uri:{ex.Uri}|");

            if(ex.HttpStatusCode != null)
                log.Append($"HttpStatusCode:{ex.HttpStatusCode.Value}|");

            if(!string.IsNullOrEmpty(ex.ContentResponse))
                log.Append($"Content:{ex.ContentResponse}|");

            return log.ToString().Substring(0, log.ToString().Length - 1);
        }

        public static string Error<Integration>(this FluentHttpContentException<Integration> ex)
            where Integration : class
        {
            var log = Logging.GetErrorHeader(typeof(FluentHttpContentException<Integration>), typeof(Integration));

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