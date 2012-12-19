var menuSlideStatus;
function funcSetSlideMenu() {
    if (menuSlideStatus != true) {
        $('#divSlide').css('display', 'none');
    }
    $('#Menu').mouseenter(function () {
        $('#Menu').css('font-size', "small");
        $('#divSlide').slideDown(0);
        $('#Menu').css('height', "19");
        menuSlideStatus = true;
    });
    $('#mainMenu').mouseleave(function () {
        $('#divSlide').slideUp(0);
        $('#Menu').css('font-size', " x-large");
        $('#Menu').css('height', "30");
        menuSlideStatus = false;
    });
}