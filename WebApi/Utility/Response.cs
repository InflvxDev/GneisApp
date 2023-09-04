namespace WebApi.Utility
{
    public class Response<T>
    {
        public bool IsSuccess { get; set; }
        public T Value { get; set; }
        public string Message { get; set; }
    }
}
