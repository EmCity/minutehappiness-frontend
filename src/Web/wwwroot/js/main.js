(function ($, Vue, urls) {

    // jQuery

    $(function () {
        $("#menu-toggle").click(function (e) {
            e.preventDefault();
            $("#wrapper").toggleClass("toggled");
        });
    });

    // YouTube IFrame Player API

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
                console.log(this.urlCollection.length);

                if (this.urlCollection.length > 0) {
                    this.videoUrl = this.urlCollection[0];
                }
            });
        }
    });

})(jQuery, Vue, urls);