using AutoMapper;
using ShoppingCart.Data;
using ShoppingCart.Services.Adapters.Interfaces;
using ShoppingCart.Services.DTOs.Product;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ShoppingCart.Services.Adapters.Concrete
{
    public class ProductsServiceAdapter : BaseServiceAdapter, IProductsServiceAdapter
    {
        private readonly IMapper _mapper;
        public ProductsServiceAdapter(AppDBContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all products from DB
        /// </summary>
        /// <returns>list product DTO</returns>
        public IQueryable<ProductDto> AllProudcts()
        {
            try
            {
                var dbEntries = DbContext.Products;
                var dtos = _mapper.Map<List<ProductDto>>(dbEntries);

                if (!dtos.Any())
                    return Enumerable.Empty<ProductDto>().AsQueryable();


                return dtos.AsQueryable();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets product by ID from DB
        /// </summary>
        /// <param name="id">product ID</param>
        /// <returns>product  DTO</returns>
        public ProductDto ProductByID(string id)
        {
            try { 
                var dbEntries = DbContext.Products.AsNoTracking().Where(p => p.Id == id);
                var dto = _mapper.Map<ProductDto>(dbEntries.FirstOrDefault());


                if (dto == null)
                    return null;


                return dto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
