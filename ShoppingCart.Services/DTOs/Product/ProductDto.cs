using System;

namespace ShoppingCart.Services.DTOs.Product
{
    public class ProductDto
    {
         public virtual string Id { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public int Quantity { get; set; }

        public double ProductPrice { get; set; }

        public string ProductImagePath { get; set; }

        public string ProductImageName { get; set; }
    }
}
