using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Store.Data.Entities.IdentityEntities;
using Store.Service.Services.TokenServices;
using Store.Service.UserServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.UserServices
{
    public class UserService : IUserService
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _manager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _token;

        public UserService(Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> manager, SignInManager<ApplicationUser> signInManager, ITokenService token)
        {
            _manager = manager;
            _signInManager = signInManager;
            _token = token;
        }
        public async Task<UserDto> Login(LoginDto dto)
        {
            var user = await _manager.FindByEmailAsync(dto.Email);
            if (user == null)
                throw new Exception("User Not Found");
            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
                throw new Exception("Faided");
            return new UserDto
            {
                DisplayName = dto.Email.Split('@')[0],
                Email = dto.Email,
                Token = _token.GenerateToken(user)
            };
        }

        public async Task<UserDto> Register(RegisterDto dto)
        {
            var user = await _manager.FindByEmailAsync(dto.Email);
            if (user != null)
                throw new Exception($"Registered {dto.Email} Already Token");
            var appuser = new ApplicationUser
            {
                Email = dto.Email,
                DisplayName = dto.UserName,
                UserName = dto.UserName,


            };
            var result = await _manager.CreateAsync(appuser, dto.Password);
            if (!result.Succeeded)
                throw new Exception("Error Faied");

            return new UserDto
            {
                Email = dto.Email,
                DisplayName = dto.UserName,
                Token = _token.GenerateToken(appuser)
            };

        }
     
    }
}