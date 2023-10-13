

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



    //................ delete product response  


    $(".deleteProduct").click(function() {
        var productId = $(this).data("product-id");
        
        $.ajax({
            type: 'DELETE',
            url: "/Admin/Product/DeleteProduct/" + productId,
            success: function () {
                const el = $(".deleted-message");
              
                el.text("Product deleted.");
                el.css("transform", "translateY(0)");
                el.css("opacity", "1");
                el.show();
                setTimeout(function () {
                    el.css("transform", "translateY(-12px)");
                    el.css("opacity", "0");
                }, 3000);
                           },
            error: function () {
              
                $("#deleted-message").text("Failed to delete product.");
                $("#deleted-message").show();
            }
        })
    })










});