using AutoMapper;
using MinuteOfHappiness.Frontend.Data.Entities;
using MinuteOfHappiness.Frontend.Web.Models;

namespace MinuteOfHappiness.Frontend.Web.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            /*
                .ForMember(dest => dest., opt => opt.MapFrom(src => src.))
                .ForMember(dest => dest., opt => opt.Ignore())
            */

            #region From entity to model

            CreateMap<Video, VideoFragmentModel>()
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.StartSecond, opt => opt.MapFrom(src => src.StartSeconds))
                .ForMember(dest => dest.EndSecond, opt => opt.MapFrom(src => src.EndSeconds));

            #endregion
        }
    }
}
