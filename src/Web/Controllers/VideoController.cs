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
            IOptions<VideoConfiguration> videoUrlConfig)
        {
            DbContext = applicationDbContext;
            Mapper = mapper;
            VideoConfiguration = videoUrlConfig.Value;
        }

        private ApplicationDbContext DbContext { get; }
        private IMapper Mapper { get; }
        private VideoConfiguration VideoConfiguration { get; }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("video/new/params")]
        public IActionResult GetNewYouTubeVideoParams()
        {
            var videoFragments = FetchNewYouTubeVideoSeries();

            // Map the entity to the model
            var model = new YouTubeVideoFragmentParamsModel()
            {
                Videos = Mapper.Map<IEnumerable<Data.Entities.Video>, IEnumerable<VideoFragmentParamsModel>>(videoFragments),
                YouTubeParams = VideoConfiguration.ParamsConfig.YouTube
            };

            return Json(model);
        }

        [HttpPost("video/new/url")]
        public IActionResult GetNewYouTubeVideoUrls()
        {
            var videoFragments = FetchNewYouTubeVideoSeries();

            // Map the entity to the model
            var model = Mapper.Map<IEnumerable<Data.Entities.Video>, IEnumerable<VideoFragmentUrlModel>>(videoFragments,
                opt =>
                {
                    opt.Items["YouTubeUrlFormat"] = VideoConfiguration.UrlConfig.YouTube;
                    opt.Items["Origin"] = $"{Request.Scheme}://{Request.Host.ToString()}";
                });

            return Json(model.Select(x => x.Url));
        }

        private IEnumerable<Data.Entities.Video> FetchNewYouTubeVideoSeries()
        {
            // Retrieve the IDs of all the videos from the database
            var videoFragmentIds = DbContext.Videos.Where(v => v.Exclude != true).Select(v => v.Id).ToList();
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

                var startDelay = videoFragment.EndSeconds - videoFragment.StartSeconds - 6;
                if (startDelay > 0)
                    videoFragment.StartSeconds += startDelay;
                
                // Add the total duration of the seconds to the variable
                totalSeconds += (videoFragment.EndSeconds - videoFragment.StartSeconds);
            }

            return videoFragmentList;
        }
    }
}
