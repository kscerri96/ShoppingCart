(function (window, $) {
    window.ShoppingCart = window.ShoppingCart || {};
    ShoppingCart.Products = ShoppingCart.Products || {};

    ShoppingCart.Products = {

        loadAllProducts: function () {
            $.ajax({
                type: "GET",
                url: "/api/Product",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.length > 0) {
                        var counter = 0;
                        $.each(data, function (i, item) {
                            var id = counter++;
                            var outerdiv = "outerdiv_" + id
                            var itemdiv = "itemdiv_" + id
                            var imgdiv = "imgdiv_" + id
                            var imgcapdiv = "imgcapdiv_" + id
                            var captiondiv = "captiondiv_" + id
                            $('<div class="col-xl-4 col-lg-4 col-md-6 col-sm-6" id=' + outerdiv + '></div>').appendTo($('#divrow'));
                            $('<div class="single-popular-items mb-50 text-center" id=' + itemdiv + '></div>').appendTo($('#' + outerdiv));
                            $('<div class="popular-img" id=' + imgdiv + '></div>').appendTo($('#' + itemdiv));
                            $('<button type="button" onclick="ShoppingCart.Products.openProductDetails(this.id)" class="btn btn-danger" id="productDetails_' + item.id + '">View Details</button>').appendTo($('#' + itemdiv));
                            $('<div class="popular-caption" id=' + captiondiv + '></div>').appendTo($('#' + itemdiv));

                            $('<img src="' + item.productImagePath + '" alt="' + item.productImageName + '">').appendTo($('#' + imgdiv));
                            $('<div class="img-cap" id=' + imgcapdiv + '></div>').appendTo($('#' + imgdiv));
                            $('<a id="link_' + item.id + '" onclick="ShoppingCart.Products.openProductDetails(this.id)"><span>Add to cart</span></a>').appendTo($('#' + imgcapdiv));
                            $('<h3>' + item.productName + '</h3>').appendTo($('#' + captiondiv));
                            $('<span>€' + item.productPrice + '</span>').appendTo($('#' + captiondiv));
                        }); //End of foreach Loop
                    }
                }, //End of AJAX Success function

                failure: function (data) {
                    alert(data.responseText);
                }, //End of AJAX failure function
                error: function (data) {
                    alert(data.responseText);
                } //End of AJAX error function

            });

        },

        openProductDetails: function (id) {
           
            var id = id.split('_')[1];
            var url = '/Home/ProductDetails?&id=' + id;
            $.get(url, function (data) {
                bootbox.dialog({
                    message: data,
                    closeButton: false,
                    buttons: {
                        success: {
                            label: 'Add To Cart',
                            className: 'btn-success',
                            callback: function () {
                                var quantity = $("input.product_count_item").val();
                                ShoppingCart.Cart.AddToCart(id, quantity);
                            }
                        },
                        cancel: {
                            label: "Cancel",
                            className: 'btn-info'
                        }
                    }
                });
            });
        },

        search: function () {
            $("#myInput").on("keyup", function () {
                var value = $(this).val().toLowerCase();
                $("[id*='captiondiv_'] h3").filter(function () {
                    
                    var included = $(this).text().toLowerCase().indexOf(value);
                    if (included > -1) {
                        $(this).parents("[id*='outerdiv_']").show();
                    }
                    else {
                        $(this).parents("[id*='outerdiv_']").hide();
                    }
                });              
            });
        }
    };


    $(document).ready(function () {
        ShoppingCart.Products.loadAllProducts();
        ShoppingCart.Products.search();

    });
})(window, jQuery);