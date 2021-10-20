using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Autransoft.Fluent.HttpClient.Lib.Exceptions;
using Newtonsoft.Json;

namespace Autransoft.Fluent.HttpClient.Lib.Fluents
{
    public class ResponseFluent : IDisposable
    {
        private readonly HttpResponseMessage _response;
        private readonly RequestFluent _request;

        public HttpStatusCode? HttpStatusCode
        {
            get => _request?.HttpStatusCode;
        }

        public ResponseFluent(HttpResponseMessage response, RequestFluent request)
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
                throw new FluentHttpContentException(ex, _request, content, _request?.HttpStatusCode);
            }
        }

        public async Task<ResponseObject> DeserializeAsync<ResponseObject>()
        {
            var content = string.Empty;

            if(_response == null)
                return default(ResponseObject);

            try
            {
                if(_response.Content != null)
                    content = await _response.Content.ReadAsStringAsync();

                if(!string.IsNullOrEmpty(content) && _response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<ResponseObject>(content);
                else
                    return default(ResponseObject);
            }
            catch(Exception ex)
            {
                throw new FluentHttpContentException(ex, _request, content, _request?.HttpStatusCode);
            }
        }

        public void Dispose() => _response.Dispose();
    }
}