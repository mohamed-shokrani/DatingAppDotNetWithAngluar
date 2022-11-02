using DatingApp.Entity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace DatingApp.Data
{
    public static class Seed
    {
        public static async Task  SeedUsers(DataContext dataContext)//returnign void from this 
        {
            if(await dataContext.Users.AnyAsync()) return;
            //if we continue it means we do not have any users in database and
            //we wanna go and interogate that file to see what we have inside there and store it in a variable  
            var UserData = await File.ReadAllTextAsync("Data/UserSeedData.json");

            var users = JsonSerializer.Deserialize<List<AppUser>> (UserData);
            foreach (var user in users)
            {
                using HMACSHA512 hmac = new HMACSHA512();

                user.UserName = user.UserName.ToLower();

                user.HashPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes("MyPassword"));

                user.PasswordSalt = hmac.Key;
                await dataContext.Users.AddAsync(user); 


            }
            await dataContext.SaveChangesAsync();



            
        }
    }
}
