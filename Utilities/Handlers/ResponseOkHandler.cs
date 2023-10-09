using System.Net;

namespace API.Utilities.Handlers
{
    public class ResponseOkHandler<TEntity>

    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public TEntity? Data { get; set; }

        public ResponseOkHandler (TEntity? data)
        {
            Code = StatusCodes.Status200OK;
            Status = HttpStatusCode.OK.ToString();
            Message = "Success to Retrieve Data";
            Data = data;
        }
        public ResponseOkHandler(string message)
        {
            Code = StatusCodes.Status200OK;
            Status = HttpStatusCode.OK.ToString();
            Message = message;
        }
    }
}
