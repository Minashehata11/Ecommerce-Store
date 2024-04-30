using Store.Service.UserServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.UserServices
{
    public interface IUserService
    {
        public Task<UserDto> Login(LoginDto dto);
        public Task<UserDto> Register(RegisterDto dto);
    }
}
