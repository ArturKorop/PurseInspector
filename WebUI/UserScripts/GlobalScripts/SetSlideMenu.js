var menuSlideStatus;
function funcSetSlideMenu() {
    if (menuSlideStatus != true) {
        $('#divSlide').css('display', 'none');
    }
    $('#Menu').mouseenter(function () {
        $('#divSlide').slideDown(10);
        menuSlideStatus = true;
    });
    $('#mainMenu').mouseleave(function () {
        $('#divSlide').slideUp(10);
        menuSlideStatus = false;
    });
}