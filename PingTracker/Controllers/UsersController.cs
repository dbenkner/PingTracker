using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PingTracker.Data;
using PingTracker.DTO;
using PingTracker.Models;

namespace PingTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly PingTrackerContext _context;

        public UsersController(PingTrackerContext context)
        {
            _context = context;
        }

        [HttpPut("login")]
        public async Task<ActionResult<User>> LoginUser(LoginDTO loginDto)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == loginDto.Username);
            if (user == null)
            {
                return Unauthorized("Invalid Username");
            }
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return Unauthorized("Invalid Password");
                }
            }
            return user;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<User>> RegisterUser(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username))
            {
                return BadRequest("Username Already Exists!");
            }
            using var hmac = new HMACSHA512();
            var user = new User()
            {
                Username = registerDto.Username.ToLower(),
                Email = registerDto.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username.ToLower());
        }
    }
}
