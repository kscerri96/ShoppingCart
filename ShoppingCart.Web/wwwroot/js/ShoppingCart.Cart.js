(function (window, $) {
    window.ShoppingCart = window.ShoppingCart || {};
    ShoppingCart.Cart = ShoppingCart.Cart || {};

    ShoppingCart.Cart = {

 
        AddToCart: function (id, quantity) {

            $.ajax({
                type: "POST",
                url: "/api/ShoppingCart/AddtoCart/" + id + "/" + quantity,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                }, //End of AJAX Success function
                failure: function (data) {
                    console.log(data.responseText);
                }, //End of AJAX failure function
                error: function (data) {
                    console.log(data.responseText);
                } //End of AJAX error function

            });

        },

        OpenCart: function () {

            $('#cd-cart-trigger').on('click', function (event) {
                event.preventDefault();
                ShoppingCart.Cart.toggle_panel_visibility($('#cd-cart'), $('#cd-shadow-layer'), $('body'));
            });  
        },

        toggle_panel_visibility: function ($lateral_panel, $background_layer, $body) {
            if ($lateral_panel.hasClass('speed-in')) {
                ShoppingCart.Cart.ClearAllShoppingCartItemshtml();
                // firefox transitions break when parent overflow is changed, so we need to wait for the end of the trasition to give the body an overflow hidden
                $lateral_panel.removeClass('speed-in').one('webkitTransitionEnd otransitionend oTransitionEnd msTransitionEnd transitionend', function () {
                    $body.removeClass('overflow-hidden');
                });
                $background_layer.removeClass('is-visible');

            } else {
                ShoppingCart.Cart.AllShoppingCartItems();
                $lateral_panel.addClass('speed-in').one('webkitTransitionEnd otransitionend oTransitionEnd msTransitionEnd transitionend', function () {
                    $body.addClass('overflow-hidden');
                });
                $background_layer.addClass('is-visible');
            }
        },

        AllShoppingCartItems: function () {
            $.ajax({
                type: "GET",
                url: "/api/ShoppingCart",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.length > 0) {
                        var counter = 0;
                        var finalPrice = 0;
                        $.each(data, function (i, item) {
                            var id = counter++;
                            var price = "price_" + id
                            var qty = "qty_" + id
                            var list = "list_" + item.productID
                            var productPrice = item.amount * item.product.productPrice;
                            finalPrice += productPrice;
                            $('<li id=' + list + '></li>').appendTo($('.cd-cart-items'));
                            $('<span class="cd-qty" id=' + qty + ' data-qty=' + item.amount + '>' + item.amount + 'x</span><p>' + item.product.productName + '</p>').appendTo($('#' + list));
                            $('<div class="cd-price" id=' + price + ' data-price=' + productPrice + '>€' + productPrice + '</div>').appendTo($('#' + list));
                            $('<a id="link_' + item.productID + '"onclick="ShoppingCart.Cart.RemoveShoppoingCartItems(this.id)" href="#0" class="cd-item-remove cd-img-replace">Remove</a>').appendTo($('#' + list));
                        }); //End of foreach Loop
                        $('<p>Total <span data-total=' + finalPrice + '>€' + finalPrice + '</span></p>').appendTo($('.cd-cart-total'));
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

        ClearShoppingCartItems: function () {
            $.ajax({
                type: "POST",
                url: "/api/ShoppingCart/ClearCart",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    ShoppingCart.Cart.ClearAllShoppingCartItemshtml();
                }, //End of AJAX Success function

                failure: function (data) {
                    alert(data.responseText);
                }, //End of AJAX failure function
                error: function (data) {
                    alert(data.responseText);
                } //End of AJAX error function

            });
        },

        RemoveShoppoingCartItems: function (id) {
            var id = id.split('_')[1];
            $.ajax({
                type: "POST",
                url: "/api/ShoppingCart/DeleteItemFromSC/" + id,
                contentType: "application/json; charset=utf-8",
                dataType: "text",
                success: function (data) {
                    ShoppingCart.Cart.updateShoppingCartData(data);
                }, //End of AJAX Success function

                failure: function (data) {
                    alert(data.responseText);
                }, //End of AJAX failure function
                error: function (data) {
                    alert(data.responseText);
                } //End of AJAX error function

            });
        },

        ClearAllShoppingCartItemshtml: function () {
            $("[id*='list_']").remove();
            $(".cd-cart-total").empty();
        },

        updateShoppingCartData: function (id) {

            var id = '#list_' + id;
            var product = $(id+ " span").data()
            var updatePrice = 0;
            var prodPrice = $(id + " div").data()
            var prevQty = product.qty;
            var pricePerItem = (prodPrice.price / product.qty);

            if (product.qty > 1) {
                updatePrice = prodPrice.price - pricePerItem;
                var finalQTy = product.qty - 1;
                $(id + " span").text(finalQTy+'x');
                $(id + " span").data("qty", finalQTy);
                $(id + " div").text('€' + updatePrice);
                $(id + " div").data("price", updatePrice);
            }

            var carTotal = $(".cd-cart-total span").data()
            var finalTotal = carTotal.total - pricePerItem;

            if ($('.cd-cart-items li').length == 1) {
                $(".cd-cart-total").empty();

            }

            $(".cd-cart-total span").text('€' + finalTotal);
            $(".cd-cart-total span").data("total", finalTotal);

            if (prevQty == 1) {
                $(id).remove();
            }
        }

    };


    $(document).ready(function () {
        ShoppingCart.Cart.OpenCart();
    });
})(window, jQuery);