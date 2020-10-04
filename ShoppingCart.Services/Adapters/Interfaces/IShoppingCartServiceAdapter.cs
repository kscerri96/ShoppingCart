using ShoppingCart.Services.DTOs.Product;
using ShoppingCart.Services.DTOs.ShoppingCartItem;
using System;
using System.Linq;

namespace ShoppingCart.Services.Adapters.Interfaces
{
    public interface IShoppingCartServiceAdapter
    {
        void AddtoCart(ProductDto dto, int amount, string ShoppingCartId);

        string RemoveFromCart(ProductDto dto, string ShoppingCartId);

        void ClearCart(string ShoppingCartId);

        IQueryable<ShoppingCartItemDto> GetShoppingCartItems(string ShoppingCartId);

        double GetShoppingCartTotal(string ShoppingCartId);


    }
}
