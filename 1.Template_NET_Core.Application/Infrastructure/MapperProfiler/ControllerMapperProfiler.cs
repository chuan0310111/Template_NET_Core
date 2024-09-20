using _1.Template_NET_Core.Application.ViewModels;
using _2.Template_NET_Core.Services.Dtos;
using AutoMapper;

namespace _1.Template_NET_Core.Application.Infrastructure.MapperProfiler
{
    /// <summary>
    /// ControllerMapperProfiler
    /// </summary>
    public class ControllerMapperProfiler : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public ControllerMapperProfiler() {

            this.CreateMap<HsinChuAreaDto, HsinChuAreaViewModel>();

        }
    }
}
