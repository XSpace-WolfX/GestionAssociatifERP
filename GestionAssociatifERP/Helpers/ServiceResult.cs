namespace GestionAssociatifERP.Helpers
{
    public enum ServiceErrorType
    {
        NotFound,
        BadRequest,
        Conflict,
        InternalError,
        Unknown
    }

    public class ServiceResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public ServiceErrorType ErrorType { get; set; } = ServiceErrorType.Unknown;

        public static ServiceResult Ok() => new()
        {
            Success = true
        };

        public static ServiceResult Fail(string message, ServiceErrorType type = ServiceErrorType.Unknown) => new()
        {
            Success = false,
            Message = message,
            ErrorType = type
        };
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; }

        public static ServiceResult<T> Ok(T data) => new()
        {
            Success = true,
            Data = data
        };

        public new static ServiceResult<T> Fail(string message, ServiceErrorType type = ServiceErrorType.Unknown) => new()
        {
            Success = false,
            Message = message,
            ErrorType = type
        };
    }
}