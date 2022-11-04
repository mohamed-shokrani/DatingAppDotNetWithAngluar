using DatingApp.DTO;
using DatingApp.Entity;
using DatingApp.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace DatingApp.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public UserRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppUser>> GetAppUsersAsync()
        {
            var users = await _dataContext.Users.Include(x=>x.Photos).ToListAsync();
            return users;
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _dataContext.Users
                 .Where(x => x.UserName == username)
                 .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                 .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return await _dataContext.Users
                            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)//when we use projection we do not need to use include 
                            //because entity framework is gonna work out correct query to join the table and get what need from the table 
                            .ToListAsync(); //
                           
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            var user = await _dataContext.Users.FindAsync(id);

            return user;
        }

        public async Task<AppUser> GetUserByNameAsync(string username)
        {
            return  await _dataContext.Users.Include(x=>x.Photos).SingleOrDefaultAsync(u=>u.UserName == username);
           
        }

        public async Task<bool> SaveAllAsync()
        {
           return await _dataContext.SaveChangesAsync() > 0;
        }

        public void UpadateUser(AppUser appUser)
        {
            _dataContext.Entry(appUser).State = EntityState.Modified; //this let entity framework update and add a flag to entity
                                                                     //
        }
    }
}
