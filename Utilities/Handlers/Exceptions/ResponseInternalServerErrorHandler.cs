using System.Net;

namespace API.Utilities.Handlers.Exceptions
{
    public class ResponseInternalServerErrorHandler : ResponseErrorHandler
    {
        public ResponseInternalServerErrorHandler(string message, string error) {
            Code = StatusCodes.Status500InternalServerError;
            Status = HttpStatusCode.InternalServerError.ToString();
            Message = message;
            Error = error;
        }
    }
}
