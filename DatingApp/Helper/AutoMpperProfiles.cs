using AutoMapper;
using DatingApp.DTO;
using DatingApp.Entity;

namespace DatingApp.Helper
{
    public class AutoMpperProfiles : Profile
    {
        public AutoMpperProfiles( )
        {
            CreateMap<AppUser,MemberDto>();
            CreateMap<Photo, PhotoDto>();

        }
    }
}
