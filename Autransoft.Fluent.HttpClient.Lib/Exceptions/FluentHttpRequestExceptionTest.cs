using System;
using System.Net;
using Autransoft.Fluent.HttpClient.Lib.Extensions;
using Autransoft.Fluent.HttpClient.Lib.Fluents;
using Autransoft.Fluent.HttpClient.Lib.Helpers;

namespace Autransoft.Fluent.HttpClient.Lib.Exceptions
{
    public class FluentHttpRequestExceptionTest : Exception
    {
        public HttpStatusCode? HttpStatusCode { get; set; }
        public string ContentResponse { get; set; }
        public string PostmanCode { get; set; }        
        public string Json { get; set; }
        public string Verb { get; set; }
        public string Uri { get; set; }

        public FluentHttpRequestExceptionTest
        (
            Exception exception, 
            RequestFluentTest requestFluent, 
            HttpStatusCode? httpStatusCode
        ) 
        : base(exception?.Message, exception)
        {
            Uri = requestFluent.Uri;
            Json = requestFluent.Json;
            Verb = requestFluent.Verb?.GetDescription();
            HttpStatusCode = httpStatusCode;
            ContentResponse = string.Empty;
            PostmanCode = PostmanHelper.GeneratePostmanCode
            (
                requestFluent.Verb.Value, 
                requestFluent.Uri, 
                requestFluent.Headers, 
                requestFluent.Token, 
                requestFluent.FormData, 
                requestFluent.Json
            );
        }

        public FluentHttpRequestExceptionTest
        (
            Exception exception, 
            RequestFluentTest requestFluent, 
            string contentRequest, 
            HttpStatusCode? httpStatusCode
        )
        : base(exception?.Message, exception)
        {
            Uri = requestFluent.Uri;
            Json = requestFluent.Json;
            Verb = requestFluent.Verb?.GetDescription();
            HttpStatusCode = httpStatusCode;
            ContentResponse = contentRequest;
            PostmanCode = PostmanHelper.GeneratePostmanCode
            (
                requestFluent.Verb.Value, 
                requestFluent.Uri, 
                requestFluent.Headers, 
                requestFluent.Token, 
                requestFluent.FormData, 
                requestFluent.Json
            );
        }
    }
}