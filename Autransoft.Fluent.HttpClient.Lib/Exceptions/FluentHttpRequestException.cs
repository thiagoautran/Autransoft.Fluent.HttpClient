using System;
using System.Net;
using Autransoft.Fluent.HttpClient.Lib.Fluents;
using Autransoft.Fluent.HttpClient.Lib.Helpers;
using Autransoft.Fluent.HttpClient.Lib.Loggings;

namespace Autransoft.Fluent.HttpClient.Lib.Exceptions
{
    public class FluentHttpRequestException : Exception
    {
        public virtual string LogError<Integration>() where Integration : class => this.Error<Integration>();

        public HttpStatusCode? HttpStatusCode { get; set; }
        public string ContentResponse { get; set; }
        public string PostmanCode { get; set; }        
        public string Json { get; set; }
        public Uri Uri { get; set; }

        public FluentHttpRequestException(Exception exception, RequestFluent requestFluent, HttpStatusCode? httpStatusCode) : base(exception.Message, exception)
        {
            Uri = requestFluent.Uri;
            Json = requestFluent.Json;
            HttpStatusCode = httpStatusCode;
            ContentResponse = string.Empty;
            PostmanCode = PostmanHelper.GeneratePostmanCode(requestFluent.Verb.Value, requestFluent.Uri, requestFluent.Headers, requestFluent.Token, requestFluent.FormData, requestFluent.Json);
        }

        public FluentHttpRequestException(Exception exception, RequestFluent requestFluent, string contentRequest, HttpStatusCode? httpStatusCode) : base(exception.Message, exception)
        {
            Uri = requestFluent.Uri;
            Json = requestFluent.Json;
            HttpStatusCode = httpStatusCode;
            ContentResponse = contentRequest;
            PostmanCode = PostmanHelper.GeneratePostmanCode(requestFluent.Verb.Value, requestFluent.Uri, requestFluent.Headers, requestFluent.Token, requestFluent.FormData, requestFluent.Json);
        }
    }
}