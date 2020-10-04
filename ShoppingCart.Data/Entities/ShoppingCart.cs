using System;
using System.Collections.Generic;

namespace ShoppingCart.Data.Entities
{
    public class ShoppingCart
    {
        private readonly AppDBContext _appDbContext;
        private ShoppingCart(AppDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
