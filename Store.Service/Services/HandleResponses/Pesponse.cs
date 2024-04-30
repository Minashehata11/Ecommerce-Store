using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.HandleResponses
{
    public class Response
    {
        public Response(int statusCode,string? Message=null)
        {
            StatusCode = statusCode;
            Message = Message?? GetDefaultMessageForStatusCode(statusCode);
        }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        private string GetDefaultMessageForStatusCode(int code)
        {
            return code switch
            {
                400 => "Bad Request",
                401 => "Unauthorized",
                403 => "Forbidden",
                404 => "Not Found",
                405 => "Method Not Allowed",
                406 => "Not Acceptable",
                408 => "Request Timeout",
                413 => "Payload Too Large",
                415 => "Unsupported Media Type",
                429 => "Too Many Requests",
                500 => "Internal Server Error",
                502 => "Bad Gateway",
                503 => "Service Unavailable",
                504 => "Gateway Timeout",
                _ => "Unknown Status Code" // Default for unlisted codes
            };
        }
    }
}
