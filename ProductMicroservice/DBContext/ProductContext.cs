using Microsoft.EntityFrameworkCore;

namespace ProductMicroservice.DBContext
{
    public class ProductContext:DbContext
    {
        public ProductContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
    }
}
