$(".editLink").click(function () {
    $(this).next(".editLinkForm").toggle();
})

$(".addLink").click(function () {
    $(this).next(".addLinkForm").toggle();
})

$(".editTopic").click(function () {
    $(this).next(".editTopicForm").toggle();
})

//$('.topicPic').hover(function () {
//    $('.topicName').addClass("font-effect-fire-animation");
//}, function () {
//    $('.topicName').removeClass("font-effect-fire-animation");
//});