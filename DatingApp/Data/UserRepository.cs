using DatingApp.Entity;
using DatingApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext dataContext;

        public UserRepository(DataContext dataContext )
        {
            this.dataContext = dataContext;
        }

        public async Task<IEnumerable<AppUser>> GetAppUsersAsync()
        {
            var users = await dataContext.Users.ToListAsync();
            return users;
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
           var user = await dataContext.Users.FindAsync(id);

            return user;
        }

        public async Task<AppUser> GetUserByNameAsync(string username)
        {
            return  await dataContext.Users.SingleOrDefaultAsync(u=>u.UserName == username);
           
        }

        public async Task<bool> SaveAllAsync()
        {
           return await dataContext.SaveChangesAsync() > 0;
        }

        public void UpadateUser(AppUser appUser)
        {
            dataContext.Entry(appUser).State = EntityState.Modified; //this let entity framework update and add a flag to entity
                                                                     //
        }
    }
}
