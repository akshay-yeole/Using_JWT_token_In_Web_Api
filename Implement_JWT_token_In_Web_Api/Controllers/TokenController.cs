using Implement_JWT_token_In_Web_Api.DataLayer;
using Implement_JWT_token_In_Web_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Implement_JWT_token_In_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ApplicationDataContext context;
        private readonly IConfiguration config;

        public TokenController(ApplicationDataContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }

        public async Task<User> GetUser(string username,string password)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
        }

        [HttpPost]
        public async Task<IActionResult> Post(User user)
        {
            var userdata = await GetUser(user.UserName,user.Password);

            if (userdata != null)
            {
                var claims = new[]{
                    new Claim(JwtRegisteredClaimNames.Sub, config["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, DateTime.UtcNow.ToString()),
                     new Claim("Id",user.UserID.ToString()),
                    new Claim("UserName",user.UserName)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));

                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(config["Jwt:Issuer"], config["Jwt:Audience"], claims, expires:DateTime.UtcNow.AddDays(1), signingCredentials : signIn);

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            else
            {
                return BadRequest("Invaid Creds are Entered");
            }
        }
    }
}
