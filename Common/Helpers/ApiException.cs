namespace Common.Helpers
{
    public class ApiException(int statusCode, string exeptionMessage, string? stackTrace)
    {
        public int StatusCode { get; set; } = statusCode;
        public string ExceptionMessage { get; set; } = exeptionMessage;
        public string? StackTrace { get; set; } = stackTrace;
    }
}
