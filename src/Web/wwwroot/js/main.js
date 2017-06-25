// YouTube IFrame Player API
// This code must be in the global scope

// Load the YouTube IFrame Player API code asynchronously.
var tag = document.createElement('script');
tag.src = "https://www.youtube.com/iframe_api";
var firstScriptTag = document.getElementsByTagName('script')[0];
firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);

function onYouTubeIframeAPIReady() {

    var youtubePlayer;

    // Vue

    var videoApp = new Vue({
        el: '#main',
        data: {
            videoCollection: [],
            videoIndex: -1,
            youTubeParams: null
        },
        computed: {
            video: function () {
                // Return the video params for the given index
                return this.videoIndex === -1 ? null : this.videoCollection[this.videoIndex];
            }
        },
        mounted: function () {
            // When loaded, fetch the video urls from the server and start playing the video
            this.fetchVideoParams(this.initYouTubePlayer);
        },
        methods: {
            fetchVideoParams: function (callback) {
                // Fetch the collection of video urls from the API
                this.$http.post(urls.getNewVideoParams).then(function (response) {

                    this.youTubeParams = response.body.youTubeParams;
                    this.videoCollection = response.body.videos;

                    // Temporarily log the amount of video urls retrieved
                    console.log('Amount of videos retrieved: ' + this.videoCollection.length);

                    callback();
                });
            },
            initYouTubePlayer: function () {

                var _this = this;

                youtubePlayer = new YT.Player('youtube-video', {
                    playerVars: _this.youTubeParams,
                    events: {
                        onReady: function (event) {
                            // Add the Bootstrap class
                            $('#youtube-video').addClass('embed-responsive-item');

                            event.target.mute();
                            _this.playNextVideo(event.target);
                        },
                        onStateChange: function (event) {
                            switch (event.data) {
                                case YT.PlayerState.ENDED:
                                    // This event is called multiple times
                                    // So only play the next video when there is currently one playing
                                    if (_this.video.playing === true) {
                                        _this.playNextVideo(event.target);
                                    }
                                    break;

                                case YT.PlayerState.PLAYING:
                                    _this.video.playing = true;
                                    break;
                            }
                        }
                    }
                });
            },
            playNextVideo: function (youTubePlayer) {
                if (this.videoIndex < this.videoCollection.length - 1) {
                    // Increase the video index by 1 when we're not at the end of the collection yet
                    this.videoIndex++;

                    youTubePlayer.loadVideoById({
                        videoId: this.video.youTubeId,
                        startSeconds: this.video.startSeconds,
                        endSeconds: this.video.endSeconds
                    });

                } else {
                    // Otherwise, end the video sequence
                    this.endVideoSequence();
                }
            },
            endVideoSequence: function () {
                console.log('Ended');
            }
        }
    });
}

(function ($) {

    // jQuery

    $(function () {
        $("#menu-toggle").click(function (e) {
            e.preventDefault();
            $("#wrapper").toggleClass("toggled");
            if ($('#wrapper').hasClass("toggled")) {
                var toggleEle = $('.negativ-margin');
                toggleEle.css('margin-top', 0);
            }
            else {
                $(window).trigger('resize');
            }
        });

        $(window).on('load resize', function () {
            if ($(document).height() > $(window).height()) {
                var ele = $('.negativ-margin');
                var newSize = $(window).height() - $(document).height();
                ele.css('margin-top', newSize + 'px');
            }
            else {
                var elseEle = $('.negativ-margin');
                elseEle.css('margin-top', 0);
            }
        });

        $(window).trigger('load');
    });

})(jQuery);