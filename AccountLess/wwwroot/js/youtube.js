$(document).ready(function () {
    $('#ytChannelsTable').DataTable({
        "pagingType": "full_numbers"
    });

});

function searchYouTubeChannel() {
    var channelSearchTerm = $("#youTubeChannelSearch").val();
    var url = "https://www.youtube.com/results?search_query=" + channelSearchTerm + "&sp=EgIQAg%253D%253D";
    var win = window.open(url, '_blank');
    win.focus();

}




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