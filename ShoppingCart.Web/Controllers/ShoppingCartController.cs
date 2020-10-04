using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Services.Adapters.Interfaces;
using ShoppingCart.Services.DTOs.Product;
using ShoppingCart.Services.DTOs.ShoppingCart;
using ShoppingCart.Services.DTOs.ShoppingCartItem;

namespace ShoppingCart.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartServiceAdapter _shoppingCart;
        private readonly IProductsServiceAdapter _service;

        public ShoppingCartController(IShoppingCartServiceAdapter shoppingCart, IProductsServiceAdapter service)
        {           
            _shoppingCart = shoppingCart;
            _service = service;
        }

        // GET: api/ShoppingCart
        /// <summary>
        /// Gets all shopping cart items
        /// </summary>
        /// <returns>a list of shopping cart items</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingCartItemDto>>> Get()
        {
            try { 
                var value = HttpContext.Session.GetString("CartId");
                var dto = _shoppingCart.GetShoppingCartItems(value.ToString());
                return await Task.FromResult(dto.ToList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetShoppingCartTotal")]
        public async Task<ActionResult<double>> GetShoppingCartTotal()
        {
            var value = HttpContext.Session.GetString("CartId");
            var total = _shoppingCart.GetShoppingCartTotal(value);
            return await Task.FromResult(total);
        }

        // POST: api/ShoppingCart
        /// <summary>
        /// Adds items to shopping cart
        /// </summary>
        /// <param name="id">item ID</param>
        /// <param name="quantity">quantity of item</param>
        [HttpPost]
        [Route("AddtoCart/{id}/{quantity}")]
        public void AddtoCart(string id, int quantity)
        {
            try { 
                var dto = _service.ProductByID(id);
                var value = HttpContext.Session.GetString("CartId");
                _shoppingCart.AddtoCart(dto, quantity, value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Clears all items from the cart
        /// </summary>
        [HttpPost("ClearCart")]
        public void ClearCart()
        {
            try { 
                var value = HttpContext.Session.GetString("CartId");
                _shoppingCart.ClearCart(value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // DELETE: api/ApiWithActions/
        /// <summary>
        /// Delete item from shopping bag
        /// </summary>
        ///// <param name="id">product id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteItemFromSC/{id}")]
        public async Task<ActionResult<string>> DeleteItemFromSC(string id)
        {
            try
            {
                var dto = _service.ProductByID(id);
                var value = HttpContext.Session.GetString("CartId");
                var total = _shoppingCart.RemoveFromCart(dto, value);
                return await Task.FromResult(total);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
