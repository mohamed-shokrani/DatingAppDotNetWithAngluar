using DatingApp.Data;
using DatingApp.DTO;
using DatingApp.Entity;
using DatingApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DatingApp.Controllers
{
    public class AccountController :BaseAPIController
    {
        private readonly DataContext _dc;
        private readonly ITokenServices _tokenServices;

        public AccountController(DataContext dc,ITokenServices tokenServices)
        {
            _dc = dc;
            _tokenServices = tokenServices;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register( RegisterDTO RegisterDto)
        {
            if(await UserExists(RegisterDto.UserName))
            {
                return BadRequest("User Name Has Taken Elly sabaq kal Elnabak");
            }
            using var hmac = new HMACSHA512();
            var USER = new AppUser
            {
                UserName = RegisterDto.UserName.ToLower(),
                HashPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(RegisterDto.Password)),
                PasswordSalt = hmac.Key

            };
           await _dc.Users.AddAsync(USER);
           await _dc.SaveChangesAsync();
            return new UserDTO
            {
                UserName = RegisterDto.UserName,
                Token = _tokenServices.GetToken(USER)
            };


        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO) 
        {
            var user = await _dc.Users.SingleOrDefaultAsync(x=>x.UserName == loginDTO.UserName);
            if (user == null) return Unauthorized("Invalid Name");

            using var hmac = new HMACSHA512(user.PasswordSalt);  
            var ComputedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

            for(int i = 0; i < ComputedHash.Length; i++)
            {
                if (ComputedHash[i] != user.HashPassword[i])
                    return Unauthorized("Invalid Password");
            }
            return new UserDTO
            {
                UserName = loginDTO.UserName,
                Token = _tokenServices.GetToken(user)
            };
        }
        private async Task<bool> UserExists(string username)
        {
            return await _dc.Users.AnyAsync(d => d.UserName == username.ToLower());
    }
    }
  
}
