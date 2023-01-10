using AutoMapper;
using DatingApp.DTO;
using DatingApp.Entity;
using DatingApp.Extensions;

namespace DatingApp.Helper
{
    public class AutoMpperProfiles : Profile
    {
        public AutoMpperProfiles( )
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(
                 src => src.Photos
                .FirstOrDefault(d=>d.IsMain).Url))
                .ForMember(dest=>dest.Age,opt=>opt.MapFrom(src=>src.DateOfBirth.CalculateAge())); //for member means which member we want to effect;
            CreateMap<Photo, PhotoDto>();
            CreateMap<MemberUpdateDto,AppUser>();

        }
    }
}
