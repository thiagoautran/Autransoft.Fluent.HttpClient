using System;
using System.Net;
using Autransoft.Fluent.HttpClient.Lib.Fluents;

namespace Autransoft.Fluent.HttpClient.Lib.Exceptions
{
    public class FluentHttpContentException : FluentHttpRequestException
    {
        public FluentHttpContentException(Exception exception, RequestFluent requestFluent, string contentRequest, HttpStatusCode? httpStatusCode) : base(exception, requestFluent, contentRequest, httpStatusCode)
        {
        }
    }
}