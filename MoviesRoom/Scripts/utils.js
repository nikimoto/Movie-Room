$(function () {
    $(".film-description").each(function () {
        var CONSTANTS = {

            MAXTEXTLENGTH:200
        }

        if ($(this).text().length >= CONSTANTS.MAXTEXTLENGTH) {
            var cuttedText = $(this).text().substr(0, CONSTANTS.MAXTEXTLENGTH - 3) + "...";
            $(this).text(cuttedText);
            $(this).after("<a href=Home/SingleFilm/" + $(this).parent().attr("value") + "> Read More </a>");
        }
    });
});