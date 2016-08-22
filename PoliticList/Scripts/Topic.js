

function mainPageUpvote(FeedLinkId, element) {
    $.ajax({
        type: 'GET',
        url: '/Home/StoryUpvote?FeedLinkId='+FeedLinkId,
        success: function (result) {
            $(element).hide();
            $(element).next().show();
            $(element).next().html(result);
        }
    });
}

function commentPageUpvote(CommentId, element) {
    $.ajax({
        type: 'GET',
        url: '/Category/CommentUpvote?CommentId=' + CommentId,
        success: function (result) {
            $(element).hide();
            $(element).next().show();
            $(element).next().html(result);
        }
    });
}

function Comment(element, FeedLinkId) {
    Post = $(element).prev().prev().val();
    $.ajax({
        type: 'GET',
        url: '/Category/Comment?Post='+Post+'&FeedLinkId='+FeedLinkId+'&slug=donald-trump',
        success: function (result) {
            console.log(result)
           
            $(element).prev().prev().before(' <div class="postBox"><div class="upvote"><button style="display:block;" class="commentHeartVoted ajaxVoted">0</button></div><p>' + result + '</p></div>');
    
            var currentVotes = $("." + FeedLinkId).text();
            $("." + FeedLinkId).text(parseInt(currentVotes) + 1);
       
        }
    });
}



$(".changeVotes").click(function () {
    $(".changeVotes").removeClass("clickable");
    $(".changeHot").addClass("clickable");
})

function showComments(FeedLinkId, element) {
    $(".commentChunk").hide();
    $("#" + FeedLinkId).show();
    $(".linkPlusDate").removeClass("commentSelected");
    $(element).parent().parent().addClass("commentSelected");
    console.log($(element).parent().parent());
}

$(".changeVotesActive").click(function () {
    $(".changeVotesActive").hide();
    $(".changeVotesInactive").show();

    $(".changeHotActive").show();
    $(".changeHotInactive").hide();



    $(".catHotNews").toggle();
    $(".catMostVotes").toggle();
})
$(".changeHotActive").click(function () {
    $(".changeHotActive").hide();
    $(".changeHotInactive").show();
    $(".changeVotesActive").show();
    $(".changeVotesInactive").hide();


    $(".catHotNews").toggle();
    $(".catMostVotes").toggle();
})



$(".editLink").click(function () {
    $(this).next(".editLinkForm").toggle();
})

$(".addLink").click(function () {
    $(this).next(".addLinkForm").toggle();
})

$(".editTopic").click(function () {
    $(this).next(".editTopicForm").toggle();
})

$(".twitterPicture").click(function () {
    twttr.events.trigger("click", {
        target: b, region: "intent", type: "click", data: {}
    })
});

        window.twttr = (function (d, s, id) {
        var t, js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) return;
        js = d.createElement(s);
        js.id = id;
        js.src = "//platform.twitter.com/widgets.js";
        fjs.parentNode.insertBefore(js, fjs);
        return window.twttr || (t = {
            _e: [],
            ready: function (f) {
                t._e.push(f)
            }
        });
    }(document, "script", "twitter-wjs"));

