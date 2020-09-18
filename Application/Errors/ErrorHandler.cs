using System;
using System.Net;

namespace Application.Error
{
    public class ErrorHandler : Exception
    {
        public ErrorHandler(HttpStatusCode statusCode, object errors = null)
        {
            StatusCode = statusCode;
            Errors = errors;
        }

        public HttpStatusCode StatusCode { get; }

        public object Errors { get; }

    }
}