$(document).ready(function () {
    $(document).on("click", ".loai-link", function (e) {
        e.preventDefault();

        $("#main-content").load($(this).data("url"));

        $(".menu li").removeClass("active");
        $(this).closest("li").addClass("active");
    });
});