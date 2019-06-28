$(document).ready(function () {
    $('#ytChannelsTable').DataTable({
        "pagingType": "full_numbers"
    });

});


//function setYoutubeSearchOption(type) {
//    $('#youtubeSearchMode').val(type.toString());
//    if (type === 'id') {
//        $('#youtubeSearchModeDD').text('Channel ID ');
//        $('#youtubeSearchModeDD').append('<span class="caret"></span>');
//    }
//    else if (type === 'username') {
//        $('#youtubeSearchModeDD').text('Username ');
//        $('#youtubeSearchModeDD').append('<span class="caret"></span>');
//    }
   
//}