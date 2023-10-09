namespace API.Utilities.Handlers.Exceptions
{
    public class ResponseErrorHandler
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string? Error { get; set; }
    }
}
