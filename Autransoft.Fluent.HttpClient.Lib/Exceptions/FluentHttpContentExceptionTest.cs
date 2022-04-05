using Autransoft.Fluent.HttpClient.Lib.Fluents;
using System;
using System.Net;

namespace Autransoft.Fluent.HttpClient.Lib.Exceptions
{
    public class FluentHttpContentExceptionTest : FluentHttpRequestExceptionTest
    {
        public FluentHttpContentExceptionTest
        (
            Exception exception, 
            RequestFluentTest requestFluent, 
            string contentRequest, 
            HttpStatusCode? httpStatusCode
        ) 
        : base(exception, requestFluent, contentRequest, httpStatusCode) { }
    }
}