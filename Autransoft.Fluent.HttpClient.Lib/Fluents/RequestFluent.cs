using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Autransoft.Fluent.HttpClient.Lib.Enums;
using Autransoft.Fluent.HttpClient.Lib.Exceptions;
using Newtonsoft.Json;

namespace Autransoft.Fluent.HttpClient.Lib.Fluents
{
    public class RequestFluent
    {
        private readonly System.Net.Http.HttpClient _httpClient;
        public System.Net.Http.HttpClient HttpClient 
        { 
            get => _httpClient; 
        }

        public Dictionary<string, string> FormData { get; private set; }
        public Dictionary<string, string> Headers { get; private set; }
        internal Verbs? Verb { get; private set; }
        public string Token { get; private set; }
        public string Json { get; private set; }
        public Uri Uri { get; private set; }
        public HttpStatusCode? HttpStatusCode { get; private set; }

        public RequestFluent(System.Net.Http.HttpClient httpClient)
        {
            _httpClient = httpClient;

            HttpStatusCode = null;
            FormData = null;
            Headers = null;
            Token = null;
            Json = null;
            Verb = null;
            Uri = null;
        }

        public RequestFluent AddCleanHeader()
        {
            _httpClient.DefaultRequestHeaders.Clear();

            return this;
        }

        public RequestFluent AddHeaders(Dictionary<string, string> headers = null)
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

        public RequestFluent AddToken(string token = null)
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

        public async Task<ResponseFluent> GetAsync(Uri uri)
        {
            Uri = uri;
            Verb = Verbs.Get;
            HttpResponseMessage response = null;

            try
            {
                response = await _httpClient.GetAsync(uri);

                HttpStatusCode = response.StatusCode;
                return new ResponseFluent(response, this);
            }
            catch(Exception ex)
            {
                throw new FluentHttpRequestException(ex, this, HttpStatusCode);
            }
        }

        public async Task<ResponseFluent> PostAsync(Uri uri, Dictionary<string, string> formData) =>
            await PostAsync<object>(uri, null, formData);

        public async Task<ResponseFluent> PostAsync<RequestObject>(Uri uri, RequestObject requestObject)
            where RequestObject : class =>
            await PostAsync(uri, requestObject, null);

        private async Task<ResponseFluent> PostAsync<RequestObject>(Uri uri, RequestObject requestObject, Dictionary<string, string> formData)
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
                    var json = JsonConvert.SerializeObject(requestObject);
                    var body = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                    response = await _httpClient.PostAsync(uri, body).ConfigureAwait(false);
                }
                else
                {
                    response = await _httpClient.PostAsync(uri, new FormUrlEncodedContent(formData));
                }

                HttpStatusCode = response.StatusCode;
                return new ResponseFluent(response, this);
            }
            catch (Exception ex)
            {
                throw new FluentHttpRequestException(ex, this, HttpStatusCode);
            }
        }
    }
}