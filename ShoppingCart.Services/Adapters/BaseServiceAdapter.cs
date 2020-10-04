using ShoppingCart.Data;

namespace ShoppingCart.Services.Adapters
{
    public abstract class BaseServiceAdapter
    {
        protected BaseServiceAdapter(AppDBContext dbContext)
        {
            DbContext = dbContext;
        }

        public AppDBContext DbContext { get; set; }
    }
}
