using Autransoft.Fluent.HttpClient.Lib.Exceptions;

namespace Autransoft.Fluent.HttpClient.Lib.Interfaces
{
    public interface IAutranSoftFluentLogger<Integration>
        where Integration : class
    {
        void LogInformation(FluentHttpContentException<Integration> fluentHttpContentException);
        void LogInformation(FluentHttpRequestException<Integration> fluentHttpRequestException);
        void LogError(FluentHttpContentException<Integration> fluentHttpContentException);
        void LogError(FluentHttpRequestException<Integration> fluentHttpRequestException);
    }
}