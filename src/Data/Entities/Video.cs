using System;
using System.Collections.Generic;

namespace MinuteOfHappiness.Frontend.Data.Entities
{
    public class Video
    {
        public int Id { get; set; }
        public string YouTubeId { get; set; }
        public int StartSeconds { get; set; }
        public int EndSeconds { get; set; }
        public bool? Exclude { get; set; }

        // Computed property to convert the start seconds to a TimeSpan
        public TimeSpan StartTime
        {
            get => TimeSpan.FromSeconds(StartSeconds);
            set => StartSeconds = (int)value.TotalSeconds;
        }

        // Computed property to convert the end seconds to a TimeSpan
        public TimeSpan EndTime
        {
            get => TimeSpan.FromSeconds(EndSeconds);
            set => EndSeconds = (int)value.TotalSeconds;
        }

        // Navigation properties
        public virtual ICollection<VideoGroup> Groups { get; set; }
    }
}
