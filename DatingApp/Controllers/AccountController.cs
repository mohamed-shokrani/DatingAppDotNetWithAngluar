using AutoMapper;
using DatingApp.Data;
using DatingApp.DTO;
using DatingApp.Entity;
using DatingApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;

namespace DatingApp.Controllers
{
    public class AccountController :BaseAPIController
    {
        private readonly DataContext _dc;
        private readonly ITokenServices _tokenServices;
        private readonly IMapper mapper;

        public AccountController(DataContext dc,ITokenServices tokenServices,IMapper mapper)
        {
            _dc = dc;
            _tokenServices = tokenServices;
            this.mapper = mapper;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register( RegisterDTO RegisterDto)
        {
            if(await UserExists(RegisterDto.UserName))
            {
                return BadRequest("User Name Has Taken Elly sabaq kal Elnabak");
            }
            var user = mapper.Map<AppUser>(RegisterDto);

            using var hmac = new HMACSHA512();


            user.UserName = RegisterDto.UserName.ToLower();
            user.HashPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(RegisterDto.Password));
            user.PasswordSalt = hmac.Key;

           
           await _dc.Users.AddAsync(user);
           await _dc.SaveChangesAsync();
            return new UserDTO
            {
                UserName = RegisterDto.UserName,
                Token = _tokenServices.GetToken(user),
                KnownAs=RegisterDto.KnownAs,

            };


        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO) 
        {
            var user = await _dc.Users
                .Include(x => x.Photos)
                .SingleOrDefaultAsync(x => x.UserName == loginDTO.UserName);
            if (user == null) return Unauthorized("Invalid Name");

            using var hmac = new HMACSHA512(user.PasswordSalt);  // what using her means that when we are finished with this praticular calss it's gonna be disposed with it correctly
            //any time we are using a class with using statement it's gonna call a method inside this class called dispose 
            //so did it dispose with this as it should do 
            // any class that uses the dispose method will imlement something called Idisposable interface 
            //dispose is an object method invoked to excute code required
            //for memory cleanup and release and reset unmanaged resources such as file handles and database connections
            var ComputedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

            for(int i = 0; i < ComputedHash.Length; i++)
            {
                if (ComputedHash[i] != user.HashPassword[i])
                    return Unauthorized("Invalid Password");
            }
            return new UserDTO
            {
                UserName = loginDTO.UserName,
                Token = _tokenServices.GetToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
                KnownAs =user.KnownAs
            };
        }
        private async Task<bool> UserExists(string username)
        {
            return await _dc.Users.AnyAsync(d => d.UserName == username.ToLower());
    }
    }
  
}
