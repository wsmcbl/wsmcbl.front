function switchTab(tabId) {
    $('.nav-link').removeClass('active');
    $('.tab-pane').removeClass('show active');

    $(`a[href="#${tabId}"]`).addClass('active');
    $(`#${tabId}`).addClass('show active');

    $('#mobileTabSelector').val(tabId);
}
$(document).ready(function () {
    const activeTab = $('.nav-link.active').attr('href');
    $('#mobileTabSelector').val(activeTab.substring(1));
});