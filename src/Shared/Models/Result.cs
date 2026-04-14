using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models
{
    public record Result<T>(bool IsSuccess, T? Value, Error? Error)
    {
        public static Result<T> Success(T value) => new Result<T>(true, value, null);
        public static Result<T> Failure(string statusCode, string message) => new Result<T>(false, default, new Error(statusCode, message));
    }

    public record Result(bool IsSuccess, Error? Error)
    {
        public static Result Success() => new Result(true, null);
        public static Result Failure(string statusCode, string message) => new Result(false, new Error(statusCode, message));
    }
}
