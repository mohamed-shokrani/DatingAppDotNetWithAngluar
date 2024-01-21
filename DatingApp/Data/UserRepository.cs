using DatingApp.DTO;
using DatingApp.Entity;
using DatingApp.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApp.Helper;

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
         
        public async Task<PageList<MemberDto>> GetMembersAsync(UserParams userParams)
        {
            var query = _dataContext.Users.AsQueryable();
            query = query.Where(u => u.UserName != userParams.CurrentUserName);
            query = query.Where(u=>u.Gender == userParams.Gender);
           
            var MaxDOB = DateTime.Today.AddYears(-userParams.MaxAge - 1);
            var MinDOB = DateTime.Today.AddYears(-userParams.MinAge);

            query = query.Where(u => u.DateOfBirth >= MaxDOB && u.DateOfBirth <= MinDOB);
            query = userParams.OrderBy switch
            {
                "created" => query.OrderBy(x => x.Created),
                _ => query.OrderByDescending(x => x.LastActive),
            };

            return await PageList<MemberDto>.CreateAsync(query.ProjectTo<MemberDto>
                (_mapper.ConfigurationProvider).AsNoTracking()
                , userParams.PageNumber, userParams.PageSize);

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
           return await _dataContext.SaveChangesAsync() > 0;//Becasue save changes async returns an integer from this particular method 
            //for the number of changes that have been changed in the database 
        }

        public void UpadateUser(AppUser appUser)
        {
            _dataContext.Entry(appUser).State = EntityState.Modified; //this let entity framework update and add a flag to entity
                                                                     //
        }
    }
}
