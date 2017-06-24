using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MinuteOfHappiness.Frontend.Data.Context;
using MinuteOfHappiness.Frontend.Web.Helpers;
using MinuteOfHappiness.Frontend.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinuteOfHappiness.Frontend.Web.Controllers
{
    [Route("")]
    public class VideoController : BaseController
    {
        public VideoController(
            ApplicationDbContext applicationDbContext, 
            IMapper mapper, 
            IOptions<VideoUrlConfiguration> videoUrlConfig)
        {
            DbContext = applicationDbContext;
            Mapper = mapper;
            VideoUrlConfiguration = videoUrlConfig.Value;
        }

        private ApplicationDbContext DbContext { get; }
        private IMapper Mapper { get; }
        private VideoUrlConfiguration VideoUrlConfiguration { get; }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("video/new")]
        public IActionResult FetchNewYouTubeVideoSeries()
        {
            // Retrieve the IDs of all the videos from the database
            var videoFragmentIds = DbContext.Videos.Select(v => v.Id).ToList();
            var videoFragmentList = new List<Data.Entities.Video>();
            var totalSeconds = 0;

            // Add video fragments to the collection as long as the total duration is below 1 minute
            // And there are still videos left
            while (totalSeconds < 60 && videoFragmentIds.Count > 0)
            {
                // Select a random index to select the ID
                var randomIdIndex = new Random().Next(videoFragmentIds.Count - 1);
                var videoFragmentId = videoFragmentIds[randomIdIndex];
                // Remove the ID from the collection so it's not reselected
                videoFragmentIds.RemoveAt(randomIdIndex);

                // Query the video fragment and add it to the collection
                var videoFragment = DbContext.Videos.Single(v => v.Id == videoFragmentId);
                videoFragmentList.Add(videoFragment);

                // Add the total duration of the seconds to the variable
                totalSeconds += (videoFragment.EndSeconds - videoFragment.StartSeconds);
            }

            // Map the entity to the model
            var model = Mapper.Map<IEnumerable<Data.Entities.Video>, IEnumerable<VideoFragmentModel>>(videoFragmentList,
                opt => opt.Items["YouTubeUrlFormat"] = VideoUrlConfiguration.YouTube);

            return Json(model.Select(x => x.Url));
        }
    }
}
