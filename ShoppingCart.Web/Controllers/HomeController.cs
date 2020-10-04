using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingCart.Services.Adapters.Interfaces;
using ShoppingCart.Services.DTOs.ShoppingCart;
using ShoppingCart.Web.Models;

namespace ShoppingCart.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductsServiceAdapter _service;

        public HomeController(ILogger<HomeController> logger, IProductsServiceAdapter service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Sets the cart id as a session variable
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            try
            {
                const string sessionKey = "CartId";
                var value = HttpContext.Session.GetString(sessionKey);
                Guid cartId = Guid.NewGuid();
                if (string.IsNullOrEmpty(value))
                    HttpContext.Session.SetString(sessionKey, cartId.ToString());

                new ShoppingCartDto() { ShoppingCartId = cartId.ToString() };
                return View();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region Partial Views
        /// <summary>
        /// Get the product and displays the details
        /// </summary>
        /// <param name="id">product id</param>
        /// <returns>render of partial view</returns>
        [HttpGet]
        public ActionResult ProductDetails(string id)
        {
            var dto = _service.ProductByID(id);
            return PartialView("_ProductDetails", dto);
        }

        #endregion Partial Views
    }
}
