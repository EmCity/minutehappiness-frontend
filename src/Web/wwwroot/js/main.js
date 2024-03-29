﻿// YouTube IFrame Player API
// This code must be in the global scope

// Load the YouTube IFrame Player API code asynchronously.
var tag = document.createElement('script');
tag.src = "https://www.youtube.com/iframe_api";
var firstScriptTag = document.getElementsByTagName('script')[0];
firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);

function onYouTubeIframeAPIReady() {

    var youtubePlayer;
    var youtubeMusic;

    // Vue

    var videoApp = new Vue({
        el: '#main',
        data: {
            videoCollection: [],
            videoIndex: -1,
            youTubeParams: null,
            playing: true,
            music: true
        },
        computed: {
            video: function () {
                // Return the video params for the given index
                return this.videoIndex === -1 ? null : this.videoCollection[this.videoIndex];
            }
        },
        mounted: function () {
            this.initYouTubeMusic(this.startVideoSequence);
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

                            $(window).trigger('resize');

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
            initYouTubeMusic: function (callback) {
                youtubeMusic = new YT.Player('youtube-music', {
                    videoId: '-96SMUqYA1k',
                    playersVars: {
                        controls: 1,
                        disablekb: 1,
                        hl: 'en',
                        rel: 0,
                        showinfo: 0,
                        loop: 1,
                        playlist: '-96SMUqYA1k'
                    },
                    events: {
                        onReady: function (event) {
                            callback();
                        },
                        onStateChange: function (event) {
                            switch (event.data) {
                                case YT.PlayerState.ENDED:
                                    event.target.playVideo();
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
            startVideoSequence: function () {
                console.log('Starting');
                this.videoIndex = -1;
                this.playing = true;
                this.music = true;
                youtubeMusic.playVideo();
                this.fetchVideoParams(this.initYouTubePlayer);
            },
            endVideoSequence: function () {
                console.log('Ended');
                this.playing = false;
                youtubeMusic.stopVideo();
                $('#youtube-video').remove();
                $('#youtube-video-container').append('<div id="youtube-video"></div>');
            },
            toggleYouTubeMusic: function () {
                if (this.music === true) {
                    youtubeMusic.pauseVideo();
                } else {
                    youtubeMusic.playVideo();
                }

                this.music = !this.music;
            }
        }
    });

    (function ($) {

        // jQuery

        $(function () {
            $("#menu-toggle").click(function (e) {
                e.preventDefault();
                $("#wrapper").toggleClass("toggled");
                if ($('#wrapper').hasClass("toggled")) {
                    var eleToggle = $('.negativ-margin');
                    eleToggle.css('margin-top', 0);
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
                    var eleElse = $('.negativ-margin');
                    eleElse.css('margin-top', 0);
                }
            });

            $(window).trigger('load');
        });

    })(jQuery);
}