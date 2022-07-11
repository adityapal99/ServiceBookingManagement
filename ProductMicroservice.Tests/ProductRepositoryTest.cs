using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ProductMicroservice;
using ProductMicroservice.DBContext;
using ProductMicroservice.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace ProductMicroserviceTest
{
    public class ProductRepositoryTest : IDisposable
    {
        private ProductContext _context;
        private ProductRepository _sut;


        public List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product() {Id=1,Name="Product1", Make="Make1", Model="Medel1", Cost=1000, CreatedDate = System.DateTime.Now},
                new Product() {Id=2,Name="Product2", Make="Make2", Model="Medel2", Cost=2000, CreatedDate = System.DateTime.Now},
                new Product() {Id=3,Name="Product3", Make="Make3", Model="Medel3", Cost=3000, CreatedDate = System.DateTime.Now},
                new Product() {Id=4,Name="Product4", Make="Make4", Model="Medel4", Cost=4000, CreatedDate = System.DateTime.Now},
            };
        }

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ProductContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _context = new ProductContext(options);
            _context.Database.EnsureCreated();
        }

        [TearDown]
        public void Teardown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();

        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task Check_GetProducts()
        {
            List<Product> products = GetProducts();
            _context.Products.AddRange(products);
            _context.SaveChanges();
            
            _sut = new ProductRepository(_context);

            var result = await _sut.GetProducts();

            Assert.That(result, Is.Not.Null);
            var returnedValue = result as List<Product>;
            Assert.That(returnedValue, Is.Not.Null);
            Assert.That(returnedValue.Count, Is.EqualTo(products.Count));
        }

        [Test]
        public async Task Check_GetProductById()
        {
            int id = 2;
            _context.Products.AddRange(GetProducts());
            _context.SaveChanges();
            _sut = new ProductRepository(_context);

            var result = await _sut.GetProductById(id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(id));
        }

        [Test]
        public async Task Check_CreateProduct()
        {
            List<Product> products = GetProducts();
            _context.Products.AddRange(products);
            _context.SaveChanges();
            _sut = new ProductRepository(_context);
            Product p = new Product() { Name = "Product1", Make = "Make1", Model = "Medel1", Cost = 1000, CreatedDate = System.DateTime.Now };
            
            await _sut.CreateProduct(p);

            Assert.That(_context.Products.Count, Is.EqualTo(5));

        }

        [Test]
        public async Task Check_DeleteProduct()
        {
            int id = 2;
            List<Product> products = GetProducts();
            _context.Products.AddRange(GetProducts());
            _context.SaveChanges();
            _sut = new ProductRepository(_context);

            await _sut.DeleteProduct(id);

            Assert.That(_context.Products.Count, Is.EqualTo(3));

        }

        [Test]
        public async Task Check_PutProduct()
        {
            int id = 1;
            List<Product> products = GetProducts();
            Product p = products.Find(x => x.Id == id);
            string newName = "Test";
            p.Name = newName;
            _context.Products.AddRange(products);
            _context.SaveChanges();
            _sut = new ProductRepository(_context);

            Product returnedProduct = await _sut.PutProduct(id, p);

            Assert.That(p, Is.EqualTo(returnedProduct));


        }
    }
}
