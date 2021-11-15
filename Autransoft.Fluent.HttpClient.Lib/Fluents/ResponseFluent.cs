using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Autransoft.Fluent.HttpClient.Lib.DTOs;
using Autransoft.Fluent.HttpClient.Lib.Exceptions;
using Autransoft.Fluent.HttpClient.Lib.Interfaces;
using Newtonsoft.Json;

namespace Autransoft.Fluent.HttpClient.Lib.Fluents
{
    public class ResponseFluent<Integration> : IDisposable
        where Integration : class
    {
        private readonly IAutranSoftFluentLogger<Integration> _logger;
        private readonly HttpResponseMessage _response;
        private readonly RequestFluent<Integration> _request;

        internal RequestFluent<Integration> Request { get => _request; }
        internal bool? LogError { get; private set; }
        internal bool? LogInfo { get; private set; }
        internal bool? ThrowEx { get; private set; }

        public HttpStatusCode? HttpStatusCode { get => _request?.HttpStatusCode; }
        
        public ResponseFluent(HttpResponseMessage response, RequestFluent<Integration> request, IAutranSoftFluentLogger<Integration> logger)
        {
            _response = response;
            _request = request;
            _logger = logger;

            LogInfo = false;
            ThrowEx = false;

            LogError = true;
        }

        public ResponseFluent<Integration> ThrowException() 
        {
            ThrowEx = true;

            return this;
        }

        public ResponseFluent<Integration> NotLogError() 
        {
            LogError = false;

            return this;
        }

        public ResponseFluent<Integration> LogInformation() 
        {
            LogInfo = true;

            return this;
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
                if(LogError != null && LogError.Value)
                    _logger.LogError(new FluentHttpContentException<Integration>(ex, _request, content, _request?.HttpStatusCode));

                if(ThrowEx != null && ThrowEx.Value)
                    throw new FluentHttpContentException<Integration>(ex, _request, content, _request?.HttpStatusCode);
                else
                    return null;
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

                if(LogInfo != null && LogInfo.Value)
                    _logger.LogInformation(new FluentHttpContentException<Integration>(null, _request, content, _request?.HttpStatusCode));

                if(!string.IsNullOrEmpty(content) && _response.IsSuccessStatusCode)
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
                if(LogError != null && LogError.Value)
                    _logger.LogError(new FluentHttpContentException<Integration>(ex, _request, content, _request?.HttpStatusCode));
                
                if(ThrowEx != null && ThrowEx.Value)
                    throw new FluentHttpContentException<Integration>(ex, _request, content, _request?.HttpStatusCode);
                else
                    return null;
            }
        }

        public void Dispose() => _response.Dispose();
    }
}