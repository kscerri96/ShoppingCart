using ShoppingCart.Services.DTOs.ShoppingCartItem;
using System;
using System.Collections.Generic;

namespace ShoppingCart.Services.DTOs.ShoppingCart
{
    public class ShoppingCartDto
    {
        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItemDto> ShoppingCartItems { get; set; }
    }
}
