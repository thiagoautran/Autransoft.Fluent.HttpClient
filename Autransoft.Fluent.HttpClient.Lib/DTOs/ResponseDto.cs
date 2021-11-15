using System.Net;

namespace Autransoft.Fluent.HttpClient.Lib.DTOs
{
    public class ResponseDto<ResponseObject>
    {
        public HttpStatusCode? HttpStatusCode { get; set; }
        public ResponseObject Data { get; set; }
        public string Content { get; set; }

        public ResponseDto(HttpStatusCode? httpStatusCode, ResponseObject responseObject, string content)
        {
            HttpStatusCode = httpStatusCode;
            Data = responseObject;
            Content = content;
        }

        public ResponseDto(HttpStatusCode? httpStatusCode, string content)
        {
            HttpStatusCode = httpStatusCode;
            Content = content;
        }
    }
}