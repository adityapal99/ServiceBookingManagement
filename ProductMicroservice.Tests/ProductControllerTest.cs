using NUnit.Framework;
using ProductMicroservice.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using ProductMicroservice.Repository;
using System.Threading.Tasks;
using ProductMicroservice;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ProductMicroservice.Models;

namespace ProductMicroserviceTest
{

    public class ProductControllerTest
    {
        Mock<IProductRepository> _repository = new Mock<IProductRepository>();
        ProductsController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new ProductsController(_repository.Object);

        }
        [Test]
        public async Task CheckGetProducts()
        {
            List<Product> products = new List<Product>()
            {
            new Product { Id = 1, Name = "A", Make = "aaa", Model = "aaa", Cost = 1000, CreatedDate = DateTime.Now },
            new Product { Id = 2, Name = "B", Make = "aaa", Model = "aaa", Cost = 1000, CreatedDate = DateTime.Now },
            new Product { Id = 3, Name = "C", Make = "aaa", Model = "aaa", Cost = 1000, CreatedDate = DateTime.Now },
            new Product { Id = 4, Name = "D", Make = "aaa", Model = "aaa", Cost = 1000, CreatedDate = DateTime.Now }
            };


            _repository.Setup(x => x.GetProducts()).ReturnsAsync(products);


            //var response = await _controller.GetProducts();
            //Assert.IsInstanceOf<OkObjectResult>(response.Result);
            //OkObjectResult result = response.Result as OkObjectResult;
            //var returedValues = result.Value as IEnumerable<Product>;
            //Assert.That(returedValues.Count(), Is.EqualTo(products.Count));
            //Assert.That(products, Is.EqualTo(returedValues));



            var response = await _controller.GetProducts();
            Assert.IsInstanceOf<OkObjectResult>(response.Result);
            var result = response.Result as OkObjectResult;
            ResponseObj converted = result.Value as ResponseObj;
            Assert.IsNotNull(converted.payload);
            var returedValues = converted.payload as IEnumerable<Product>;
            Assert.That(returedValues.Count(), Is.EqualTo(products.Count));
            Assert.That(products, Is.EqualTo(returedValues));
        }

        [Test]
        public async Task CheckGetProductById_ProductPresent()
        {
            int id = 1;
            Product p = new Product { Id = 1, Name = "A", Make = "aaa", Model = "aaa", Cost = 1000, CreatedDate = DateTime.Now };
            _repository.Setup(x => x.GetProductById(id)).ReturnsAsync(p);

            //var response = await _controller.GetProduct(id);
            //Assert.IsInstanceOf<OkObjectResult>(response.Result);
            //var result = response.Result as OkObjectResult;
            //Product returedValue = result.Value as Product;
            //Assert.That(returedValue.Id, Is.EqualTo(id));
            //Assert.That(p, Is.EqualTo(returedValue));

            var response = await _controller.GetProduct(id);
            Assert.IsInstanceOf<OkObjectResult>(response.Result);
            var result = response.Result as OkObjectResult;
            ResponseObj converted = result.Value as ResponseObj;
            Assert.IsNotNull(converted.payload);
            var returedValue = converted.payload as Product;
            Assert.That(p, Is.EqualTo(returedValue));

        }

        [Test]
        public async Task CheckGetProductById_ProductMissing()
        {
            int id = 6;
            
            _repository.Setup(x => x.GetProductById(id)).ReturnsAsync((Product)null);

            var response = await _controller.GetProduct(id);
            Assert.IsInstanceOf<NotFoundResult>(response.Result);
        }

        [Test]
        public async Task CheckPutProduct_ValidInputs()
        {
            Product product = new Product { Id = 1, Name = "A", Make = "aaa", Model = "aaa", Cost = 1000, CreatedDate = DateTime.Now };

            _repository.Setup(x => x.PutProduct(product.Id, product)).ReturnsAsync(product);

            //var response = await _controller.PutProduct(product.Id, product);
            //Assert.IsInstanceOf<OkObjectResult>(response.Result);
            //var result = response.Result as OkObjectResult;
            //var returedValue = result.Value as Product;
            //Assert.That(product.Id, Is.EqualTo(returedValue.Id));

            var response = await _controller.PutProduct(product.Id,product);
            Assert.IsInstanceOf<OkObjectResult>(response.Result);
            var result = response.Result as OkObjectResult;
            ResponseObj converted = result.Value as ResponseObj;
            Assert.IsNotNull(converted.payload);
            var returedValue = converted.payload as Product;
            Assert.That(returedValue, Is.EqualTo(product));
        }
        [Test]
        public async Task CheckPutProduct_InvalidInputs()
        {
            int id = 6;
            Product product = new Product { Id = 1, Name = "A", Make = "aaa", Model = "aaa", Cost = 1000, CreatedDate = DateTime.Now };

            _repository.Setup(x => x.PutProduct(id, product)).ReturnsAsync(product);

            var response = await _controller.PutProduct(id, product);
            Assert.IsInstanceOf<BadRequestResult>(response.Result);
        }
        [Test]
        public async Task CheckPostProduct_ValidInputs()
        {
            Product product = new Product { Name = "A", Make = "aaa", Model = "aaa", Cost = 1000, CreatedDate = DateTime.Now };
            Product productFinal = new Product { Id = 1, Name = "A", Make = "aaa", Model = "aaa", Cost = 1000, CreatedDate = DateTime.Now };

            _repository.Setup(x => x.CreateProduct(product)).ReturnsAsync(productFinal);

            //var response = await _controller.PostProduct(product);
            //Assert.IsInstanceOf<CreatedAtActionResult>(response.Result);
            //var result = response.Result as CreatedAtActionResult;
            //var returedValue = result.Value as Product;
            //Assert.IsNotNull(returedValue.Id);

            var response = await _controller.PostProduct(product);
            Assert.IsInstanceOf<CreatedAtActionResult>(response.Result);
            var result = response.Result as CreatedAtActionResult;
            ResponseObj converted = result.Value as ResponseObj;
            Assert.IsNotNull(converted.payload);
            var returedValue = converted.payload as Product;
            Assert.That(returedValue, Is.EqualTo(productFinal));
        }
        [Test]
        public async Task CheckPostProduct_InvalidInputs()
        {
            Product product = new Product();

            _repository.Setup(x => x.CreateProduct(product)).ReturnsAsync(product);

            var response = await _controller.PostProduct(product);
            Assert.IsInstanceOf<BadRequestResult>(response.Result);
        }

        [Test]
        public async Task CheckDeleteProduct_ProductPresent()
        {
            Product product = new Product { Id = 1, Name = "A", Make = "aaa", Model = "aaa", Cost = 1000, CreatedDate = DateTime.Now };

            _repository.Setup(x => x.DeleteProduct(product.Id)).ReturnsAsync(product);

            //var response = await _controller.DeleteProduct(product.Id);
            //Assert.IsInstanceOf<OkObjectResult>(response.Result);
            //var result = response.Result as OkObjectResult;
            //var returedProduct = result.Value as Product;
            //Assert.That(product.Id, Is.EqualTo(returedProduct.Id));
            //Assert.That(returedProduct, Is.EqualTo(product));

            var response = await _controller.DeleteProduct(product.Id);
            Assert.IsInstanceOf<OkObjectResult>(response.Result);
            var result = response.Result as OkObjectResult;
            var responseObj = result.Value as ResponseObj;
            var returedProduct = responseObj.payload as Product;
            Assert.That(product.Id, Is.EqualTo(returedProduct.Id));
            Assert.That(returedProduct, Is.EqualTo(product));
        }

        [Test]
        public async Task CheckDeleteProduct_ProductMissing()
        {
            int id = 10;

            _repository.Setup(x => x.DeleteProduct(id)).ReturnsAsync((Product)null);

            //var response = await _controller.DeleteProduct(id);
            //Assert.IsInstanceOf<NotFoundResult>(response.Result);

            var response = await _controller.DeleteProduct(id);
            Assert.IsInstanceOf<NotFoundResult>(response.Result);
        }
    }
}