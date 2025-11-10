namespace MizeBazi.Store.Common.Shared;

public class AppException : Exception
{
    public Result result { get; set; }

    public AppException(int code = -1, string message = "") : base(message)
    {
        result = Result.Failure(code: code, message: message);
    }
    public AppException(int code = -1, List<string> errors = null) : base("error")
    {

        result = Result.Failure(code: code, errors: errors);

    }
    public static AppException Error(string message = "Error", int code = -1) => new AppException(code: code, message: message);
    public static AppException Error(List<string> errors = null, int code = -1) => new AppException(code: code, errors: errors);
    public static AppException BadRequest(string message = "Bad Request") => new AppException(code: 400, message: message);
}