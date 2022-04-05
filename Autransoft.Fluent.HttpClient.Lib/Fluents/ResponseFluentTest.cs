using Autransoft.Fluent.HttpClient.Lib.DTOs;
using Autransoft.Fluent.HttpClient.Lib.Exceptions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Autransoft.Fluent.HttpClient.Lib.Fluents
{
    public class ResponseFluentTest : IDisposable
    {
        private readonly HttpResponseMessage _response;
        private readonly RequestFluentTest _request;

        internal RequestFluentTest Request { get => _request; }

        public HttpStatusCode? HttpStatusCode { get => _request?.HttpStatusCode; }
        
        public ResponseFluentTest(HttpResponseMessage response, RequestFluentTest request)
        {
            _response = response;
            _request = request;
        }

        public async Task<ResponseDto<string>> ContentAsStringAsync()
        {
            var content = string.Empty;

            if(_response == null)
                return null;

            try
            {
                if(_response.Content != null)
                    content = await _response.Content.ReadAsStringAsync();

                return new ResponseDto<string>(_request.HttpStatusCode, content);
            }
            catch(Exception ex)
            {
                throw new FluentHttpContentExceptionTest(ex, _request, content, _request?.HttpStatusCode);
            }
        }

        public async Task<ResponseDto<ResponseObject>> DeserializeAsync<ResponseObject>()
        {
            var content = string.Empty;

            if(_response == null)
                return null;

            try
            {
                if(_response.Content != null)
                    content = await _response.Content.ReadAsStringAsync();

                if(!string.IsNullOrEmpty(content))
                {
                    if(_request.UseNewtonsoft != null && _request.UseNewtonsoft.Value)
                        return new ResponseDto<ResponseObject>(_request.HttpStatusCode, JsonConvert.DeserializeObject<ResponseObject>(content), content);
                    else
                        return new ResponseDto<ResponseObject>(_request.HttpStatusCode, System.Text.Json.JsonSerializer.Deserialize<ResponseObject>(content), content);
                }
                else
                {
                    return new ResponseDto<ResponseObject>(_request.HttpStatusCode, content);
                }
            }
            catch(Exception ex)
            {
                throw new FluentHttpContentExceptionTest(ex, _request, content, _request?.HttpStatusCode);
            }
        }

        public void Dispose() => _response.Dispose();
    }
}