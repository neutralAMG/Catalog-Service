

using Catalog.Service.Domain.Enums;

namespace Catalog.Service.Features.Core
{
    public class Result
    {

        public Result(string message, bool isSuccess)
        {
            Message = message;
            IsSuccess = isSuccess;
        }

        public string Message { get; }
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;

        public static Result Success(string message)
        {
            return new Result(message, true);
        }
        public static Result Failure(string message) {
            return new Result(message, false);
        }
    }

    public class Result<TData>
    {

        public Result(string message, bool isSuccess, TData? data)
        {
            Message = message;
            IsSuccess = isSuccess;
            Data = data;
        }
        public string Message { get; }
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public TData? Data { get; }

        public static Result<TData> Success(string message, TData data) => new(message, true, data);

        public static Result<TData> Failure(string message) => new(message, false, default);

        public static implicit operator Result<TData>(Result result) => new(result.Message, result.IsSuccess, default);

        public static implicit operator Result<TData>(TData data) => new("Success", true, data);
    }
}
