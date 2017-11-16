$(document).ajaxStart(function () {
    startLoading();
});

$(document).ajaxSuccess(function () {
    stopLoading();
});