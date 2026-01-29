namespace apis.Models
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }

        public string? Message { get; set; }

        public T Data { get; set; }

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


        public Result(bool isSuccess, string? message, T data) : this(isSuccess, message)
        {
            Data = data;
        }

        public static Result<T> Success(string message, T data) => new(true, message, data);

        public static Result<T> Failure(string message) => new(false, message, default);
    }
}
