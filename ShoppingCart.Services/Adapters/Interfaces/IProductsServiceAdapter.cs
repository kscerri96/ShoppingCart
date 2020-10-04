using ShoppingCart.Services.DTOs.Product;
using System;
using System.Linq;

namespace ShoppingCart.Services.Adapters.Interfaces
{
    public interface IProductsServiceAdapter
    {
        IQueryable<ProductDto> AllProudcts();

        ProductDto ProductByID(string id);
    }
}
