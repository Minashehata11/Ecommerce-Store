using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.HandleResponses
{
    public class ValidationErrorResonse : CustomException
    {
        public ValidationErrorResonse() : base(400)
        {
        }

        public IEnumerable<string> Errors { get; set; } 
    }
}
