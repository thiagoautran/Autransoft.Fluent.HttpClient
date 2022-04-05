using Autransoft.Fluent.HttpClient.Lib.Extensions;
using Autransoft.Fluent.HttpClient.Lib.Fluents;
using Autransoft.Fluent.HttpClient.Lib.Helpers;
using Autransoft.Fluent.HttpClient.Lib.Loggings;
using System;
using System.Net;

namespace Autransoft.Fluent.HttpClient.Lib.Exceptions
{
    public class FluentHttpRequestException<Integration> : Exception
        where Integration : class
    {
        public virtual string LogError() => this.Error<Integration>();
        public virtual string LogInformation() => this.Info<Integration>();

        public HttpStatusCode? HttpStatusCode { get; set; }
        public string ContentResponse { get; set; }
        public string PostmanCode { get; set; }        
        public string Json { get; set; }
        public string Verb { get; set; }
        public Uri Uri { get; set; }

        public FluentHttpRequestException
        (
            Exception exception, 
            RequestFluent<Integration> requestFluent, 
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

        public FluentHttpRequestException
        (
            Exception exception, 
            RequestFluent<Integration> requestFluent, 
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