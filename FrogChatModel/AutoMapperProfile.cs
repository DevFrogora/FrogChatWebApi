using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FrogChatModel.DomainModel;

namespace FrogChatModel
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            //CreateMap<SignUpUserDto, TblUser>();
            //CreateMap<TblUser, SignUpUserDto>();

            //CreateMap< IEnumerable<TblUser>, IEnumerable<DTOUser> >();

        }

    }
}
