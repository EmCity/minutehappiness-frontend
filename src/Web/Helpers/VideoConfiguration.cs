using System.Collections.Generic;

namespace MinuteOfHappiness.Frontend.Web.Helpers
{
    public class VideoConfiguration
    {
        public UrlConfiguration UrlConfig { get; set; }
        public ParamsConfiguration ParamsConfig { get; set; }

        public class UrlConfiguration
        {
            public string YouTube { get; set; }
        }

        public class ParamsConfiguration
        {
            public IDictionary<string, object> YouTube { get; set; }
        }
    }
}
