using System;
using System.Text;

namespace Autransoft.Fluent.HttpClient.Lib.Loggings
{
    internal static class Logging
    {
        public static StringBuilder GetInformationHeader()
        {
            var log = new StringBuilder();

            log.Append($"Type:Information|");

            return log;
        }

        public static StringBuilder GetErrorHeader(Type exception, Type integration)
        {
            var log = new StringBuilder();

            log.Append($"Type:Error|");
            log.Append($"Exception:{exception.Name}|");
            log.Append($"Class:{integration.Name}|");

            return log;
        }

        public static void GetExceptionMessage(StringBuilder log, Exception ex)
        {
            if(!string.IsNullOrEmpty(ex.Message))
                log.Append($"Message:{ex.Message}|");

            if(ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                log.Append($"InnerException.Message:{ex.InnerException.Message}|");

            if(ex.InnerException != null && ex.InnerException.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.InnerException.Message))
                log.Append($"InnerException.InnerException.Message:{ex.InnerException.InnerException.Message}|");

            if(ex.InnerException != null && ex.InnerException.InnerException != null && ex.InnerException.InnerException.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.InnerException.InnerException.Message))
                log.Append($"InnerException.InnerException.InnerException.Message:{ex.InnerException.InnerException.InnerException.Message}|");

            if(!string.IsNullOrEmpty(ex.StackTrace))
                log.Append($"StackTrace:{ex.StackTrace}|");
        }
    }
}