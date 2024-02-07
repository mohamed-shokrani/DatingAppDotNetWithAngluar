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
            CreateMap<RegisterDTO, AppUser>();

            CreateMap<Message,MessageDto>()
                .ForMember(des=>des.SenderPhotoUrl,opt=>opt
                .MapFrom(src=>src.Sender.Photos
                .FirstOrDefault(x=>x.IsMain).Url))
            .ForMember(des => des.RecipientPhotoUrl, opt => opt
                .MapFrom(src => src.Recipient.Photos
                .FirstOrDefault(x => x.IsMain).Url));


        }
    }
}
