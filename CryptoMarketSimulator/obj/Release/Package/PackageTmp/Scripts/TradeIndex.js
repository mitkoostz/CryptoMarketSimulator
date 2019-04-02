

$(document).ready(function () {

    $(".img-rounded").hover(function () {

        $(this).css("border", "1px solid white");
        $(this).css("border-radius", "30px");


    }, function () {

        $(this).css("border", "none");
        $(this).css("border-radius", "0px");



    });
});
