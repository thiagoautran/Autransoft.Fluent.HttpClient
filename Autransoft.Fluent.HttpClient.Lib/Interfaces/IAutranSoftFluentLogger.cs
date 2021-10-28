using Autransoft.Fluent.HttpClient.Lib.Exceptions;

namespace Autransoft.Fluent.HttpClient.Lib.Interfaces
{
    public interface IAutranSoftFluentLogger<Repository>
        where Repository : class
    {
        void LogError(FluentHttpContentException fluentHttpContentException);
        void LogError(FluentHttpRequestException fluentHttpRequestException);
    }
}