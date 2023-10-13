

// home page 
function loadProductsForCategory(category) {
    $("[data-category-products]").hide();
    $("[data-category-products='" + category + "']").show();

    $.get("/Customer/Home/GetProductsByCategory?category=" + category, function (data) {
        $("[data-category-products='" + category + "']").html(data);
    });
}

$(document).ready(function () {

    // start  home page ............
    var initialCategory = $(".category-link:first").data("category");
    loadProductsForCategory(initialCategory);


    $(".category-link").click(function () {
        var category = $(this).data("category");
        loadProductsForCategory(category);
    });
    // end home page .........


    $("#scroll-left").click(function () {
        $(".category-container").animate({ scrollLeft: "-=100" }, "slow");
    });

    $("#scroll-right").click(function () {
        $(".category-container").animate({ scrollLeft: "+=100" }, "slow");
    });
});