using Autransoft.Fluent.HttpClient.Lib.Fluents;
using Autransoft.Fluent.HttpClient.Lib.Loggings;
using System;
using System.Net;

namespace Autransoft.Fluent.HttpClient.Lib.Exceptions
{
    public class FluentHttpContentException<Integration> : FluentHttpRequestException<Integration>
        where Integration : class
    {
        public override string LogError() => this.Error<Integration>();
        public override string LogInformation() => this.Info<Integration>();

        public FluentHttpContentException
        (
            Exception exception, 
            RequestFluent<Integration> requestFluent, 
            string contentRequest, 
            HttpStatusCode? httpStatusCode
        ) 
        : base(exception, requestFluent, contentRequest, httpStatusCode) { }
    }
}