using Microsoft.EntityFrameworkCore;
using ShoppingCart.Data.Entities;

namespace ShoppingCart.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {
        }

        public AppDBContext()
        {
        }

        public DbSet<Products> Products { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //    => options.UseSqlite("Data Source=C:/Users/kscerri/source/repos/ShoppingCart/ShoppingCart.Data/shoppingcarttask.db");
    }
}
