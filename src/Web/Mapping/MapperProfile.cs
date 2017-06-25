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

            #region VideoFragmentParamsModel

            CreateMap<Video, VideoFragmentParamsModel>()
                .ForMember(dest => dest.YouTubeId, opt => opt.MapFrom(src => src.YouTubeId))
                .ForMember(dest => dest.StartSeconds, opt => opt.MapFrom(src => src.StartSeconds))
                .ForMember(dest => dest.EndSeconds, opt => opt.MapFrom(src => src.EndSeconds));

            #endregion

            #region VideoFragmentUrlModel

            CreateMap<Video, VideoFragmentUrlModel>()
                .ForMember(dest => dest.Url, opt => opt.ResolveUsing((src, dest, mem, res) =>
                {
                    var youTubeUrlFormat = res.Items["YouTubeUrlFormat"] as string;
                    var origin = res.Items["Origin"] as string;

                    return youTubeUrlFormat?.Replace("{videoId}", src.YouTubeId)
                        .Replace("{startSeconds}", src.StartSeconds.ToString())
                        .Replace("{endSeconds}", src.EndSeconds.ToString())
                        .Replace("{origin}", origin ?? "_");
                }));

            #endregion

            #endregion
        }
    }
}
