using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Autransoft.Fluent.HttpClient.Lib.DTOs;
using Autransoft.Fluent.HttpClient.Lib.Exceptions;
using Autransoft.Fluent.HttpClient.Lib.Loggings;
using Newtonsoft.Json;

namespace Autransoft.Fluent.HttpClient.Lib.Fluents
{
    public class ResponseFluent<Integration> : IDisposable
        where Integration : class
    {
        private readonly HttpResponseMessage _response;
        private readonly RequestFluent<Integration> _request;

        internal RequestFluent<Integration> Request { get => _request; }

        public HttpStatusCode? HttpStatusCode { get => _request?.HttpStatusCode; }

        public ResponseFluent(HttpResponseMessage response, RequestFluent<Integration> request)
        {
            _response = response;
            _request = request;
        }

        public async Task<string> ContentAsStringAsync()
        {
            var content = string.Empty;

            if(_response == null)
                return default(string);

            try
            {
                if(_response.Content != null)
                    content = await _response.Content.ReadAsStringAsync();

                if(!string.IsNullOrEmpty(content) && _response.IsSuccessStatusCode)
                    return content;
                else
                    return default(string);
            }
            catch(Exception ex)
            {
                throw new FluentHttpContentException<Integration>(ex, _request, content, _request?.HttpStatusCode);
            }
        }

        public async Task<ResponseDto<ResponseObject>> DeserializeAsync<ResponseObject>()
        {
            var content = string.Empty;

            if(_response == null)
                return default(ResponseDto<ResponseObject>);

            try
            {
                if(_response.Content != null)
                    content = await _response.Content.ReadAsStringAsync();

                if(!string.IsNullOrEmpty(content) && _response.IsSuccessStatusCode)
                {
                    if(_request.UseNewtonsoft != null && _request.UseNewtonsoft.Value)
                        return new ResponseDto<ResponseObject>(_request.HttpStatusCode, JsonConvert.DeserializeObject<ResponseObject>(content));
                    else
                        return new ResponseDto<ResponseObject>(_request.HttpStatusCode, System.Text.Json.JsonSerializer.Deserialize<ResponseObject>(content));
                }
                else
                {
                    return default(ResponseDto<ResponseObject>);
                }
            }
            catch(Exception ex)
            {
                _request.HttpClient.Dispose();
                _request.HttpClient = null;
                
                throw new FluentHttpContentException<Integration>(ex, _request, content, _request?.HttpStatusCode);
            }
            finally
            {
                _request.HttpClient.Dispose();
                _request.HttpClient = null;
            }
        }

        public void Dispose() => _response.Dispose();
    }
}