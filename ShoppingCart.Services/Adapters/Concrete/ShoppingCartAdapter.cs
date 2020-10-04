using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Data;
using ShoppingCart.Data.Entities;
using ShoppingCart.Services.Adapters.Interfaces;
using ShoppingCart.Services.DTOs.Product;
using ShoppingCart.Services.DTOs.ShoppingCart;
using ShoppingCart.Services.DTOs.ShoppingCartItem;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart.Services.Adapters.Concrete
{
    public class ShoppingCartAdapter : BaseServiceAdapter, IShoppingCartServiceAdapter
    {
        private readonly IMapper _mapper;
        public ShoppingCartAdapter(AppDBContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Adds product to cart and saves them in DB
        /// </summary>
        /// <param name="dto">product DTO</param>
        /// <param name="amount">quantity amount</param>
        /// <param name="ShoppingCartId">shopping cart ID</param>
        public void AddtoCart(ProductDto dto, int amount, string ShoppingCartId)
        {
            try
            {
                var dbEntries = DbContext.ShoppingCartItems.SingleOrDefault(s => s.ProductID == dto.Id && s.ShoppingCartId == ShoppingCartId);
                var shoppingCartItemDto = _mapper.Map<ShoppingCartItemDto>(dbEntries);

                if (shoppingCartItemDto == null)
                {
                    shoppingCartItemDto = new ShoppingCartItemDto
                    {
                        ShoppingCartId = ShoppingCartId,
                        Amount = amount,
                        ProductID = dto.Id
                    };


                    var shoppingCartItem = _mapper.Map<ShoppingCartItem>(shoppingCartItemDto);

                    DbContext.ShoppingCartItems.Add(shoppingCartItem);
 
                }
                else
                {
                    dbEntries.Amount += amount;
                }

                DbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }         
        }

        /// <summary>
        /// Remove selected product from cart
        /// </summary>
        /// <param name="dto">product dto</param>
        /// <param name="ShoppingCartId">shopping cart id</param>
        /// <returns></returns>
        public string RemoveFromCart(ProductDto dto, string ShoppingCartId)
        {
            try
            {
                var dbEntries = DbContext.ShoppingCartItems.SingleOrDefault(s => s.ProductID == dto.Id && s.ShoppingCartId == ShoppingCartId);
                var shoppingCartItemDto = _mapper.Map<ShoppingCartItemDto>(dbEntries);

                if (shoppingCartItemDto != null)
                {
                    var shoppingCartItem = _mapper.Map<ShoppingCartItem>(dbEntries);
                    if (shoppingCartItemDto.Amount > 1)
                    {
                        shoppingCartItem.Amount = shoppingCartItemDto.Amount - 1;
                        //localAmount = shoppingCartItemDto.Amount;
                    }
                    else
                    {
                        DbContext.ShoppingCartItems.Remove(shoppingCartItem);
                    }

                    DbContext.SaveChanges();
                    return dbEntries.ProductID;
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Clear all items from cart 
        /// </summary>
        /// <param name="ShoppingCartId">shopping cart ID</param>
        public void ClearCart(string ShoppingCartId)
        {
            try
            {
                var cartItems = DbContext
                    .ShoppingCartItems
                    .Where(cart => cart.ShoppingCartId == ShoppingCartId);

                DbContext.ShoppingCartItems.RemoveRange(cartItems);

                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get all shopping cart items
        /// </summary>
        /// <param name="ShoppingCartId">shopping cart id</param>
        /// <returns>list of shopping cart items</returns>
        public IQueryable<ShoppingCartItemDto> GetShoppingCartItems(string ShoppingCartId)
        {
            try
            {
                var dbEntries = DbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId).Include(s => s.Product);

                var dtos = _mapper.Map<List<ShoppingCartItemDto>>(dbEntries);

                if (!dtos.Any())
                    return Enumerable.Empty<ShoppingCartItemDto>().AsQueryable();


                return dtos.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public double GetShoppingCartTotal(string ShoppingCartId)
        {
            var total = DbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Product.ProductPrice * c.Amount).Sum();
            return total;
        }

    }
}
