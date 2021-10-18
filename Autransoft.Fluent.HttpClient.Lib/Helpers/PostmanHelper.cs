using System;
using System.Collections.Generic;
using System.Text;
using Autransoft.Fluent.HttpClient.Lib.Enums;
using Autransoft.Fluent.HttpClient.Lib.Extensions;

namespace Autransoft.Fluent.HttpClient.Lib.Helpers
{
    internal static class PostmanHelper
    {
        internal static string GeneratePostmanCode(Verbs verb, Uri uri, Dictionary<string, string> headers, string token, Dictionary<string, string> formData, string json)
        {
            var postman = new StringBuilder();

            postman.Append($@"curl --location --request {verb.GetDescription()} {uri} \ ");

            if(headers != null)
            {
                foreach(var key in headers.Keys)
                    if(!string.IsNullOrEmpty(key) && headers[key] != null)
                        postman.Append($@"--header '{key}: {headers[key]}' \ ");
            }

            if(formData != null)
            {
                foreach(var key in formData.Keys)
                    if(!string.IsNullOrEmpty(key) && formData[key] != null)
                        postman.Append($@"--header '{key}: {formData[key]}' \ ");
            }
            
            if(!string.IsNullOrEmpty(json))
                postman.Append($@"--data-raw '{json}' \ ");

            if(!string.IsNullOrEmpty(token))
                postman.Append($@"--header 'Authorization: Bearer {token}' \ ");

            if(postman.Length > 0)
                return postman.ToString().Substring(0, postman.Length - 2);
            else
                return string.Empty;
        }
    }
}