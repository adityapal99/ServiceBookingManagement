using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductMicroservice.Repository
{
    public interface IProductRepository
    {
        public Task<Product> CreateProduct(Product product);
        public Task<Product> DeleteProduct(int id);
        public Task<Product> GetProductById(int id);
        public Task<IEnumerable<Product>> GetProducts();
        public Task<Product> PutProduct(int id, Product product);
    }
}