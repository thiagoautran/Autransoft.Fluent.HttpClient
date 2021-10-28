using System;
using System.Net;
using Autransoft.Fluent.HttpClient.Lib.Fluents;
using Autransoft.Fluent.HttpClient.Lib.Loggings;

namespace Autransoft.Fluent.HttpClient.Lib.Exceptions
{
    public class FluentHttpContentException : FluentHttpRequestException
    {
        public override string LogError<Integration>() where Integration : class => this.Error<Integration>();

        public FluentHttpContentException(Exception exception, RequestFluent requestFluent, string contentRequest, HttpStatusCode? httpStatusCode) : base(exception, requestFluent, contentRequest, httpStatusCode)
        {
        }
    }
}