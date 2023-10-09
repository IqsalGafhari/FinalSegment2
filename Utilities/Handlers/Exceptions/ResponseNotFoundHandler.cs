using System.Net;

namespace API.Utilities.Handlers.Exceptions
{
    public class ResponseNotFoundHandler : ResponseErrorHandler
    {
        public ResponseNotFoundHandler(string message) {
            Code = StatusCodes.Status404NotFound;
            Status = HttpStatusCode.NotFound.ToString();
            Message = message;
        }
    }
}
