namespace apis.Models
{
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
    }
}
