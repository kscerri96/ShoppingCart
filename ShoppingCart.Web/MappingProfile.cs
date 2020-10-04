using AutoMapper;
using ShoppingCart.Data.Entities;
using ShoppingCart.Services.DTOs.Product;
using ShoppingCart.Services.DTOs.ShoppingCart;
using ShoppingCart.Services.DTOs.ShoppingCartItem;

namespace ShoppingCart.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Products, ProductDto>();
            CreateMap<ProductDto, Products>();

            CreateMap<Data.Entities.ShoppingCart, ShoppingCartDto>();
            CreateMap<ShoppingCartDto, Data.Entities.ShoppingCart>();

            CreateMap<ShoppingCartItem, ShoppingCartItemDto>();
            CreateMap<ShoppingCartItemDto, ShoppingCartItem>();
        }  
    }
}
