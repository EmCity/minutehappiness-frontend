(function ($, Vue, urls) {

    // jQuery

    $(function () {
        $("#menu-toggle").click(function (e) {
            e.preventDefault();
            $("#wrapper").toggleClass("toggled");
            if ($('#wrapper').hasClass("toggled")) {
                var ele = $('.negativ-margin');
                ele.css('margin-top', 0);
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
                var ele = $('.negativ-margin');
                ele.css('margin-top', 0);
            }
        });

        $(window).trigger('load');
    });

    // YouTube IFrame Player API

    /*function onYouTubeIframeAPIReady() {
        var player = new YT.Player('youtube-video', {
            height: '390',
            width: '640',
            videoId: 'M7lc1UVf-VE',
            events: {
                'onReady': onPlayerReady,
                'onStateChange': onPlayerStateChange
            }
        });
    }*/

    // Vue

    new Vue({
        el: '#main',
        data: {
            videoUrl: '',
            urlCollection: [],
        },
        mounted: function () {
            this.$http.post(urls.getNewVideo).then(function (response) {

                this.urlCollection = response.body;

                // Temporarily log the amount of video urls retrieved
                console.log('Amount of videos retrieved: ' + this.urlCollection.length);

                if (this.urlCollection.length > 0) {
                    this.videoUrl = this.urlCollection[0];
                }
            });
        }
    });

})(jQuery, Vue, urls);