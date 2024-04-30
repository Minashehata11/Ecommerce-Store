using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.UserServices.Dtos
{
    public class RegisterDto
    {
        [EmailAddress]
        public string Email { get; set; }
        
        public string Password { get; set; }

        public string UserName { get; set; }

    }
}
