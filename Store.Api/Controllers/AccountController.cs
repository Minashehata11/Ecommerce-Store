using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Store.Data.Entities.IdentityEntities;
using Store.Service.UserServices;
using Store.Service.UserServices.Dtos;
using System.Security.Claims;

namespace Store.Api.Controllers
{
  
    public class AccountController : BaseController
    {
        private readonly IUserService _service;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _manager;

        public AccountController(IUserService service,Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> manager)
        {
            _service = service;
            _manager = manager;
        }
        [HttpPost]
        public async Task<ActionResult<UserDto>> Login(LoginDto dto)
        => await _service.Login(dto);

        [HttpPost]
        public async Task<ActionResult<UserDto>> register(RegisterDto dto)
            => await _service.Register(dto);

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetCurrentUser()
        {
            var email =User?.FindFirstValue(ClaimTypes.Email);
            var user = await _manager.FindByEmailAsync(email);

            var i=  new
            {
                Email = user.Email,
                User=user.UserName
            };


             return Ok(i);

        }
    }
}
