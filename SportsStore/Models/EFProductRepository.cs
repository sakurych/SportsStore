namespace SportsStore.Models
{
    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext context;
        public EFProductRepository(ApplicationDbContext ctx)
        {
            this.context = ctx;
        }
        public IEnumerable<Product> Products => context.Products;
    }
}
