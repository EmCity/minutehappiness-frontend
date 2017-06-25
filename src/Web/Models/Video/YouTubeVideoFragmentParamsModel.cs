using System.Collections.Generic;

namespace MinuteOfHappiness.Frontend.Web.Models
{
    public class YouTubeVideoFragmentParamsModel
    {
        public IEnumerable<VideoFragmentParamsModel> Videos { get; set; }
        public object YouTubeParams { get; set; }
    }
}
