using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Autransoft.Fluent.HttpClient.Lib.Enums;
using Autransoft.Fluent.HttpClient.Lib.Exceptions;
using Autransoft.Fluent.HttpClient.Lib.Interfaces;
using Newtonsoft.Json;

namespace Autransoft.Fluent.HttpClient.Lib.Fluents
{
    public class RequestFluent<Integration> : IDisposable
        where Integration : class
    {
        private readonly IAutranSoftFluentLogger<Integration> _logger;
        
        private System.Net.Http.HttpClient _httpClient;
        internal System.Net.Http.HttpClient HttpClient { get => _httpClient; set => _httpClient = value; }

        public Dictionary<string, string> FormData { get; private set; }
        public Dictionary<string, string> Headers { get; private set; }
        internal Verbs? Verb { get; private set; }
        public string Token { get; private set; }
        public string Json { get; private set; }
        public Uri Uri { get; private set; }
        internal bool? UseNewtonsoft { get; private set; }
        public HttpStatusCode? HttpStatusCode { get; private set; }

        public RequestFluent(System.Net.Http.HttpClient httpClient, IAutranSoftFluentLogger<Integration> logger)
        {
            _httpClient = httpClient;
            _logger = logger;

            HttpStatusCode = null;
            FormData = null;
            Headers = null;
            Token = null;
            Json = null;
            Verb = null;
            Uri = null;

            UseNewtonsoft = false;
        }

        public RequestFluent<Integration> ConvertWithNewtonsoft() 
        {
            UseNewtonsoft = true;

            return this;
        }

        public RequestFluent<Integration> CleanDefaultRequestHeaders()
        {
            _httpClient.DefaultRequestHeaders.Clear();

            return this;
        }

        public RequestFluent<Integration> AddDefaultRequestHeaders(Dictionary<string, string> headers = null)
        {
            if(headers != null)
            {
                foreach(var key in headers.Keys)
                {
                    if(!string.IsNullOrEmpty(key) && headers[key] != null && !string.IsNullOrEmpty(headers[key]))
                        _httpClient.DefaultRequestHeaders.Add(key, headers[key]);
                }
            }

            Headers = headers;

            return this;
        }

        public string GetToken(string token)
        {
            if(!string.IsNullOrEmpty(token))
            {
                if(token.ToUpper().Contains("BEARER "))
                    return $"Bearer {token.Substring(6, token.Length - 6).Trim()}";
                else
                    return $"Bearer {token}";
            }

            return string.Empty;
        }

        public RequestFluent<Integration> AddBearerToken(string token = null)
        {
            if(!string.IsNullOrEmpty(token))
            {
                if(token.ToUpper().Contains("BEARER "))
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Substring(6, token.Length - 6).Trim());
                else
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            Token = token;

            return this;
        }

        public async Task<ResponseFluent<Integration>> GetAsync(Uri uri)
        {
            Uri = uri;
            Verb = Verbs.Get;
            HttpResponseMessage response = null;

            try
            {
                response = await _httpClient.GetAsync(uri);

                HttpStatusCode = response.StatusCode;
                return new ResponseFluent<Integration>(response, this, _logger);
            }
            catch(Exception ex)
            {
                _logger.LogError(new FluentHttpRequestException<Integration>(ex, this, HttpStatusCode));
                return new ResponseFluent<Integration>(null, null, null);
            }
        }

        public async Task<ResponseFluent<Integration>> PostAsync(Uri uri, Dictionary<string, string> formData) =>
            await PostAsync<object>(uri, null, formData);

        public async Task<ResponseFluent<Integration>> PostAsync<RequestObject>(Uri uri, RequestObject requestObject)
            where RequestObject : class =>
            await PostAsync(uri, requestObject, null);

        private async Task<ResponseFluent<Integration>> PostAsync<RequestObject>(Uri uri, RequestObject requestObject, Dictionary<string, string> formData)
            where RequestObject : class
        {
            Uri = uri;
            FormData = formData;
            Verb = Verbs.Post;
            HttpResponseMessage response = null;

            try
            {
                if(requestObject != null)
                {
                    var json = string.Empty;
                    
                    if(UseNewtonsoft != null && UseNewtonsoft.Value)
                        json = JsonConvert.SerializeObject(requestObject);
                    else
                        json = System.Text.Json.JsonSerializer.Serialize(requestObject);

                    var body = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                    response = await _httpClient.PostAsync(uri, body).ConfigureAwait(false);
                }
                else
                {
                    response = await _httpClient.PostAsync(uri, new FormUrlEncodedContent(formData));
                }

                HttpStatusCode = response.StatusCode;
                return new ResponseFluent<Integration>(response, this, _logger);
            }
            catch (Exception ex)
            {
                _httpClient.Dispose();
                _httpClient = null;

                _logger.LogError(new FluentHttpRequestException<Integration>(ex, this, HttpStatusCode));
                return new ResponseFluent<Integration>(null, null, null);
            }
        }

        public void Dispose()
        {
            //if(_httpClient != null)
            //{
            //    _httpClient.Dispose();
            //    _httpClient = null;
            //}

            HttpStatusCode = null;
            FormData = null;
            Headers = null;
            Token = null;
            Verb  = null;
            Json = null;
            Uri = null;

            UseNewtonsoft = null;
        }
    }
}