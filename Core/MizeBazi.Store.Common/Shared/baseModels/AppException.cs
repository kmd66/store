using MizeBazi.Store.Common.Constants;
using MizeBazi.Store.Common.Helper;

namespace MizeBazi.Store.Common.Shared;

public record ExceptionRecord(bool success, string message, int code, int totalCount);

public class AppException : Exception
{
    public Result result { get; set; }

    public AppException(int code = -1, string message = "") : base(message)
    {
        result = Result.Failure(code: code, message: message);
    }

    public static ExceptionRecord Response(string message, int code = -1) => new(false, message, code, 0);
}

public class AppTokenException : Exception
{
    public AppTokenException(string message = "") : base(message) { }
    public static ExceptionRecord Response() => AppException.Response("دسترسی غیر مجاز", 401);
}

public class AppNotFoundException : Exception
{
    public AppNotFoundException(string message = "") : base(message) { }
    public static ExceptionRecord Response() => AppException.Response("منبع مورد نظر یافت نشد", 404);
}

public class AppTimeoutException : Exception
{
    public AppTimeoutException(string message = "") : base(message) { }
    public static ExceptionRecord Response() => AppException.Response("زمان درخواست به پایان رسید", 408);
}
//----------------
public class DbException : Exception
{
    public DbException(string message = "") : base(message) { }
    public static ExceptionRecord Response(string message) => AppException.Response(AppSetings.IsDevelopment ? message : ErrorMsg.DbError);
}
public class ValidatorException : Exception
{
    public ValidatorException(string message) : base(message) { }
    public static ExceptionRecord Response(string message) => AppException.Response(message);
}