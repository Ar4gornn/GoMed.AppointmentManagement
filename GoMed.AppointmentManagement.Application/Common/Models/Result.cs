using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoMed.AppointmentManagement.Application.Common.Models;

public class Result
{
    public bool IsSuccess { get; }
    public ErrorDetail? Error { get; }

    protected Result(bool isSuccess, ErrorDetail? error = null)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    // Factory methods for non-generic Result
    public static Result Success() => new Result(true);
    public static Result Failure(ErrorDetail error) => new Result(false, error);

    public static Result ValidationError(Dictionary<string, string[]> validationErrors) =>
        Failure(new ErrorDetail(
            ErrorType.Validation,
            "ValidationError",
            "One or more validation errors occurred.",
            validationErrors
        ));

    public static Result NotFound(string code, string message) =>
        Failure(new ErrorDetail(ErrorType.NotFound, code, message));

    public static Result Conflict(string code, string message) =>
        Failure(new ErrorDetail(ErrorType.Conflict, code, message));

    public static Result Forbidden(string code, string message) =>
        Failure(new ErrorDetail(ErrorType.Forbidden, code, message));

    public static Result BadRequest(string code, string message) =>
        Failure(new ErrorDetail(ErrorType.BadRequest, code, message));

    public TR Match<TR>(
        Func<TR> onSuccess,
        Func<ErrorDetail, TR> onFailure)
    {
        return IsSuccess ? onSuccess() : onFailure(Error!);
    }

    public IResult ToIResult()
    {
        return IsSuccess
            ? Results.Ok()
            : Error!.Type switch
            {
                ErrorType.Validation => Results.ValidationProblem(Error.ValidationErrors ?? new()),

                ErrorType.NotFound => Results.NotFound(new ProblemDetails
                {
                    Title = "Not Found",
                    Detail = Error.Message,
                    Status = StatusCodes.Status404NotFound,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4"
                }),
                ErrorType.Conflict => Results.Conflict(new ProblemDetails
                {
                    Title = "Conflict",
                    Detail = Error.Message,
                    Status = StatusCodes.Status409Conflict,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.8"
                }),
                _ => Results.Problem(new ProblemDetails
                {
                    Title = "An error occurred",
                    Detail = Error.Message,
                    Status = StatusCodes.Status500InternalServerError,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
                })
            };
    }
}

public class Result<T> : Result
{
    public T? Value { get; }

    private Result(bool isSuccess, T? value = default, ErrorDetail? error = null)
        : base(isSuccess, error)
    {
        Value = value;
    }

    // Factory methods for generic Result<T>
    public static Result<T> Success(T value) => new Result<T>(true, value);
    public new static Result<T> Failure(ErrorDetail error) => new Result<T>(false, default, error);

    public new static Result<T> ValidationError(Dictionary<string, string[]> validationErrors) =>
        Failure(new ErrorDetail(
            ErrorType.Validation,
            "ValidationError",
            "One or more validation errors occurred.",
            validationErrors
        ));

    public new static Result<T> NotFound(string code, string message) =>
        Failure(new ErrorDetail(ErrorType.NotFound, code, message));

    public new static Result<T> Conflict(string code, string message) =>
        Failure(new ErrorDetail(ErrorType.Conflict, code, message));

    public new static Result<T> Forbidden(string code, string message) =>
        Failure(new ErrorDetail(ErrorType.Forbidden, code, message));

    public new static Result<T> BadRequest(string code, string message) =>
        Failure(new ErrorDetail(ErrorType.BadRequest, code, message));

    public TR Match<TR>(
        Func<T, TR> onSuccess,
        Func<ErrorDetail, TR> onFailure)
    {
        return IsSuccess ? onSuccess(Value!) : onFailure(Error!);
    }

    public new IResult ToIResult()
    {
        return IsSuccess
            ? Results.Ok(Value)
            : Error!.Type switch
            {
                ErrorType.Validation => Results.ValidationProblem(Error.ValidationErrors ?? new()),

                ErrorType.NotFound => Results.NotFound(new ProblemDetails
                {
                    Title = "Not Found",
                    Detail = Error.Message,
                    Status = StatusCodes.Status404NotFound,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4"
                }),
                ErrorType.Conflict => Results.Conflict(new ProblemDetails
                {
                    Title = "Conflict",
                    Detail = Error.Message,
                    Status = StatusCodes.Status409Conflict,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.8"
                }),
                _ => Results.Problem(new ProblemDetails
                {
                    Title = "An error occurred",
                    Detail = Error.Message,
                    Status = StatusCodes.Status500InternalServerError,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
                })
            };
    }
}