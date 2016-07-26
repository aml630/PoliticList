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

