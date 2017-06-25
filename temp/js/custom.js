$("#menu-toggle").click(function(e) {
    e.preventDefault();
    $("#wrapper").toggleClass("toggled");
    $(window).trigger('resize');
});

$(window).on('load resize', function(){
    var ele = $('.negativ-margin');
    ele.css('margin-top', 50 + 'px');

    if ($(document).height() > $(window).height()) {
        var ele = $('.negativ-margin');
        var newSize = $(window).height()-$(document).height()+50;
        ele.css('margin-top', newSize + 'px');
    }
});

$(window).trigger('load');