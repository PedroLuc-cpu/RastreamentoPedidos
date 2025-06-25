using RastreamentoPedido.Core.Communication;
using System.Net;

namespace Clients.Web.Exceptions
{
    public class CustomHttpRequestException : Exception
    {
        public HttpStatusCode StatusCode;
        private string message = string.Empty;
        public override string Message { get { return message; } }

        public CustomHttpRequestException() { }

        public CustomHttpRequestException(string message, Exception innerException) : base(message, innerException) { }

        public CustomHttpRequestException(HttpStatusCode statusCode) => StatusCode = statusCode;

        public CustomHttpRequestException(ResponseResult response)
        {
            StatusCode = (HttpStatusCode)response.Status;
            string msg = string.Empty;
            foreach (var key in response.Errors.Keys)
            {
                var delimiter = string.Empty;
                foreach (var item in response.Errors[key])
                {
                    msg += item + delimiter;
                    delimiter = ";\r\n";
                }
            }
            message = msg;
        }
    }
}
