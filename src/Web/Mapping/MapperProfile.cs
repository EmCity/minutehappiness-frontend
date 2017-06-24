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
                .ForMember(dest => dest.Url, opt => opt.ResolveUsing((src, dest, mem, res) =>
                {
                    var youTubeUrlFormat = res.Items["YouTubeUrlFormat"] as string;

                    return youTubeUrlFormat?.Replace("{videoId}", src.YouTubeId)
                        .Replace("{startSeconds}", src.StartSeconds.ToString())
                        .Replace("{endSeconds}", src.EndSeconds.ToString());
                }));

            #endregion
        }
    }
}
