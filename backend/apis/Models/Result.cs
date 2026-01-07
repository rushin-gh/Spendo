namespace apis.Models
{
    public class Result
    {
        public bool IsSuccess { get; set; }

        public string? Msg { get; set; }

        public Result()
        {
            IsSuccess = true;
            Msg = string.Empty;
        }

        public Result(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public Result(bool isSuccess, string? msg) : this(isSuccess)
        {
            Msg = msg;
        }
    }
}
