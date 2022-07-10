using System.Collections.Generic;
using System.Threading.Tasks;
namespace ProductMicroservice.Repository
{
    public class InMemoryRepository : IProductRepository
    {
        public static int id = 5;
        public static List<Product> Products = new List<Product>
        {
            new Product() {Id=1,Name="Product1", Make="Make1", Model="Medel1", Cost=1000, CreatedDate = System.DateTime.Now},
            new Product() {Id=2,Name="Product2", Make="Make2", Model="Medel2", Cost=2000, CreatedDate = System.DateTime.Now},
            new Product() {Id=3,Name="Product3", Make="Make3", Model="Medel3", Cost=3000, CreatedDate = System.DateTime.Now},
            new Product() {Id=4,Name="Product4", Make="Make4", Model="Medel4", Cost=4000, CreatedDate = System.DateTime.Now},
        };
        public async Task<Product> CreateProduct(Product product)
        {

            product.Id = id;
            id++;
            Products.Add(product);
            return await Task.FromResult(product);
        }

        public async Task<Product> DeleteProduct(int id)
        {
            Product product = Products.Find(x => x.Id == id);
            Products.Remove(product);
            return await Task.FromResult(product);    
        }

        public async Task<Product> GetProductById(int id)
        {
            Product product = Products.Find(x => x.Id == id);
            return await Task.FromResult(product);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            IEnumerable<Product> products = Products;
            return await Task.FromResult(products);

        }

        public async Task<Product> PutProduct(int id, Product product)
        {
            Product Original = Products.Find(x => x.Id == id);
            Original.Name = product.Name;
            Original.Cost = product.Cost;
            Original.Make = product.Make;
            Original.CreatedDate = product.CreatedDate;
            return await Task.FromResult(product);
        }
    }
}
