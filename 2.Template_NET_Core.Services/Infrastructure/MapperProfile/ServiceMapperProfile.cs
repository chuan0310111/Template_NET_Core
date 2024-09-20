using _2.Template_NET_Core.Services.Dtos;
using _3.Template_NET_Core.Repositories.DataModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.Template_NET_Core.Services.Infrastructure.MapperProfile
{
    public class ServiceMapperProfile : Profile
    {
        public ServiceMapperProfile() {

            this.CreateMap<HsinChuAreaDataModel, HsinChuAreaDto>().ForMember(x => x.鄉鎮市公所名稱_new, otp => otp.MapFrom(src => src.鄉鎮市公所名稱));
        }
    }
}
