namespace apis.Models
{
    public enum StatusCodes
    {
        Ok = 200
    }

    public class Result
    {
        public bool IsSuccess { get; set; }

        public string? Message { get; set; }

        public Result()
        {
            IsSuccess = true;
            Message = string.Empty;
        }

        public Result(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public Result(bool isSuccess, string? msg) : this(isSuccess)
        {
            Message = msg;
        }

        public static Result Success(string message) => new(true, message);

        public static Result Failure(string message) => new(false, message);
    }

    public class DataResult <T> : Result
    {
        public T? Data { get; set; }

        public DataResult()
        {
            IsSuccess = true;
            Message = "NA";
            Data = default;
        }

        public DataResult(bool isSuccess) : base()
        {
            IsSuccess = isSuccess;
        }

        public DataResult(bool isSuccess, string? msg) : base(isSuccess)
        {
            Message = msg;
        }

        public DataResult(bool isSuccess, string? msg, T? data) : base(isSuccess, msg)
        {
            Data = data;
        }

        public static DataResult<T> Success(string message, T? data) => new(true, message, data);

        public static DataResult<T> Failure(string message) => new(false, message);
    }
}
