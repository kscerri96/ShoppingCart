using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Services.Adapters.Interfaces;
using ShoppingCart.Services.DTOs.Product;

namespace ShoppingCart.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductsServiceAdapter _service;
        public ProductController(IProductsServiceAdapter service)
        {
            _service = service;
        }

        //GET: api/Product
        /// <summary>
        /// Gets all products
        /// </summary>
        /// <returns>all products</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
        {
            try
            {
                var dto = _service.AllProudcts();
                return await Task.FromResult(dto.ToList());
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        // GET: api/Product/5
        /// <summary>
        /// Gets products by ID
        /// </summary>
        /// <param name="id">product ID</param>
        /// <returns>product DTO</returns>
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<ProductDto>> Get(string id)
        {
            try
            {
                var dto = _service.ProductByID(id);
                return await Task.FromResult(dto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
