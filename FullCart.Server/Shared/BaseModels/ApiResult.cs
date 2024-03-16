using FluentValidation.Results;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FullCart.Server.Shared.BaseModels;

public class ApiResult
{
    public static Result Success() => new(true);

    public static Result Success<T>(T data) => new ResultWithData<T>(data);

    public static Result Fail(params ErrorResult[] errors) => new ResultWithError(errors);
    public static Result Fail(IEnumerable<ValidationFailure> validationFailures)
        => Fail(validationFailures.Select(e => new ErrorResult(e.PropertyName, e.ErrorMessage)).ToArray());
}


public record Result(bool IsSuccess);

public record ResultWithData<T>(T Data) : Result(true);

public record ResultWithError(IEnumerable<ErrorResult> Errors) : Result(false);

public record ErrorResult(string Code, string? Description);
