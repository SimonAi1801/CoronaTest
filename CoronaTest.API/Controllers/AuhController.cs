using CoronaTest.Core.DTOs;
using CoronaTest.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace CoronaTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuhController : ControllerBase
    {

        private static List<AuthUser> _users = new List<AuthUser>
        {
            new AuthUser{ Email = "admin@htl.at", Password = AuthUtils.GenerateHashedPassword("admin"), Role = "Admin" },
            new AuthUser { Email = "user@htl.at", Password = AuthUtils.GenerateHashedPassword("user"), Role = "User" }
        };
        private IConfiguration _config;

        public AuhController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(AuthUserDto userDto)
        {
            var authUser = _users.SingleOrDefault(u => u.Email == userDto.Email);

            if (authUser == null)
            {
                return Unauthorized();
            }

            if (!AuthUtils.VerifyPassword(userDto.Password, authUser.Password))
            {
                return Unauthorized();
            }

            var tokenString = GenerateJWTToken(authUser);

            IActionResult response = Ok(new 
            {
                auth_token = tokenString,
                userEmail = authUser.Email
            });

            return response;
        }

        private string GenerateJWTToken(AuthUser userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var authClaims = new List<Claim>();
            authClaims.Add(new Claim(ClaimTypes.Email, userInfo.Email));
            authClaims.Add(new Claim(ClaimTypes.Country, "Austria"));
            if (!string.IsNullOrEmpty(userInfo.Role))
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userInfo.Role));
            }

            var token = new JwtSecurityToken(
              issuer: _config["Jwt:Issuer"],
              audience: _config["Jwt:Audience"],
              claims: authClaims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
