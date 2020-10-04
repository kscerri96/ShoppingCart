using ShoppingCart.Services.DTOs.Product;
using System;

namespace ShoppingCart.Services.DTOs.ShoppingCartItem
{
    public class ShoppingCartItemDto
    {
        public int ShoppingCartItemId { get; set; }
        public string ProductID { get; set; }
        public ProductDto Product { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
