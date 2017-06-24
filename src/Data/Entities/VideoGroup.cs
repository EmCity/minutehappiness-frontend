using System.Collections.Generic;

namespace MinuteOfHappiness.Frontend.Data.Entities
{
    public class VideoGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation properties
        public virtual ICollection<Video> Videos { get; set; }
    }
}
