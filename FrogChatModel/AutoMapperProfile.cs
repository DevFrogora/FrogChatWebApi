using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FrogChatModel.DomainModel;
using FrogChatModel.DTOModel;

namespace FrogChatModel
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            //CreateMap<SignUpUserDto, TblUser>();
            //CreateMap<TblUser, SignUpUserDto>();

            //CreateMap< IEnumerable<TblUser>, IEnumerable<DTOUser> >();
            //CreateMap<UserDto,ApplicationUser>
            CreateMap<SignUpUserDto, UserDto>()
    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
    .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.PhotoPath))
    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
        }

    }
}
