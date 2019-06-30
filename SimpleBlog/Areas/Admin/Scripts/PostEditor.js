$(document).ready(function () {

    var $tagEditor = $(".post-tag-editor");

    $tagEditor
        .find(".tag-select")
        .on("click", "> li > a", function (e) {
            e.preventDefault();

            var $this = $(this);
            var $tagParent = $this.closest("li");
            $tagParent.toggleClass("selected");

            var selected = $tagParent.hasClass("selected");
            $tagParent.find(".selected-input").val(selected);


        }); 
});