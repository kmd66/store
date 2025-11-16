using MizeBazi.Store.Common.Constants;
using MizeBazi.Store.Common.Helper;

namespace MizeBazi.Store.Common.Shared;
public static class ExceptionStaticClass
{
    public static AppException ExceptionHandler(this Exception exception)
    {
        var record = Response(exception.Message);
        switch (exception)
        {
            case AppTokenException:
            case UnauthorizedAccessException:
                record = AppTokenException.Response();
                break;

            case AppNotFoundException:
            case KeyNotFoundException:
                record = AppNotFoundException.Response();
                break;

            case AppBadRequest:
            case ArgumentException:
            case InvalidOperationException:
                record = AppBadRequest.Response(exception.Message);
                break;

            case AppTimeoutException:
            case TimeoutException:
                record = AppTimeoutException.Response();
                break;

            case DbException:
            case ValidatorException:
                record = DbException.Response(exception.Message);
                break;

            case AppException:
                return exception as AppException;
            default:
                record = Response(exception.Message, code: 500);
                break;
        }
        return new AppException(record);
    }
    public static ExceptionRecord Response(string message, int code = -1) => new(false, message, code, 0);
}

public record ExceptionRecord(bool success, string message, int code, int totalCount);

public class AppException : Exception
{
    public ExceptionRecord Record { get; set; }

    public AppException(ExceptionRecord record) : base(record.message)
    {
        Record = record;
    }

    public AppException(int code = -1, string message = "") : base(message)
    {
        Record = new(false, message, code, 0);
    }

}
public class AppBadRequest : AppException
{
    public AppBadRequest(string message = "") : base(message: message, code: 400) { }
    public static ExceptionRecord Response(string message = "")
        => ExceptionStaticClass.Response(message.IsNullOrEmpty() ? "درخواست نامناسب" : message, 400);
}

public class AppTokenException : AppException
{
    public AppTokenException(string message = "") : base(message: message, code: 401) { }
    public static ExceptionRecord Response() => ExceptionStaticClass.Response("دسترسی غیر مجاز", 401);
}

public class AppNotFoundException : AppException
{
    public AppNotFoundException(string message = "") : base(message: message, code: 404) { }
    public static ExceptionRecord Response() => ExceptionStaticClass.Response("منبع مورد نظر یافت نشد", 404);
}

public class AppTimeoutException : AppException
{
    public AppTimeoutException(string message = "") : base(message: message, code: 408) { }
    public static ExceptionRecord Response() => ExceptionStaticClass.Response("زمان درخواست به پایان رسید", 408);
}
//----------------
public class DbException : AppException
{
    public DbException(string message = "") : base(message: message) { }
    public static ExceptionRecord Response(string message) => ExceptionStaticClass.Response(AppSetings.IsDevelopment ? message : ErrorMsg.DbErrorMsg);
}
public class ValidatorException : AppException
{
    public ValidatorException(string message) : base(message: message) { }
    public static ExceptionRecord Response(string message) => ExceptionStaticClass.Response(message);
}