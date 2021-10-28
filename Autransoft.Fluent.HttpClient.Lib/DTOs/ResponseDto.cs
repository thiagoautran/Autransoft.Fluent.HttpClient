using System.Net;

namespace Autransoft.Fluent.HttpClient.Lib.DTOs
{
    public class ResponseDto<ResponseObject>
    {
        public HttpStatusCode? HttpStatusCode { get; set; }
        public ResponseObject Data { get; set; }

        public ResponseDto(HttpStatusCode? httpStatusCode, ResponseObject responseObject)
        {
            HttpStatusCode = httpStatusCode;
            Data = responseObject;
        }
    }
}