using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.HandleResponses
{
    public class CustomException : Response
    {
        public CustomException(int statusCode, string? Message = null,string? details=null) : base(statusCode, Message)
        {
            Details = details;
        }
        public string Details { get; set; }
    }
}
