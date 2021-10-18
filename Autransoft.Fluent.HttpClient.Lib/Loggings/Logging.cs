using System;
using System.Text;

namespace Autransoft.Fluent.HttpClient.Lib.Loggings
{
    public static class Logging
    {
        public static StringBuilder GetHeader(Type type)
        {
            var log = new StringBuilder();

            log.Append($"Type:Error|");
            log.Append($"Exception:{type.Name}|");

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

            if(!string.IsNullOrEmpty(ex.StackTrace))
                log.Append($"StackTrace:{ex.StackTrace}|");
        }
    }
}