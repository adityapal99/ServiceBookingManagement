using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductMicroservice;
using ProductMicroservice.DBContext;
using ProductMicroservice.Repository;

namespace ProductMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ProductsController));
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                _log4net.Info("GetProducts Method Called");
                IEnumerable<Product> products = await _repository.GetProducts();
                return Ok(new { status = 200, msg = "All products", payload = products });
            }
            catch
            {
                _log4net.Error("Database error");
                return StatusCode(500);
            }
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                _log4net.Info("GetProduct Method Called");
                var product = await _repository.GetProductById(id);

                if (product == null)
                {
                    return NotFound();
                }

                return Ok(new { status = 200, msg = "Product Found", payload = product });
            }
            catch
            {
                _log4net.Error("Database error");
                return StatusCode(500);
            }
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            _log4net.Info("PutProduct Method called");
            if (id != product.Id)
            {
                return BadRequest();
            }
            try
            {
                await _repository.PutProduct(id, product);
                return Ok(new { status = 200, msg = "Update Successful", payload = product });
            }
            catch
            {
                _log4net.Error("Databse error");
                return StatusCode(500);
            }
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromBody]Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _log4net.Info("PostProduct Method Called");
                   /* Product p = new Product();
                    p.Name = product.Name;
                    p.Make = product.Make;
                    p.Model = product.Model;
                    p.Cost = product.Cost;
                    p.CreatedDate = new DateTime();*/
                    Product productWithId = await _repository.CreateProduct(product);
                    //Product productWithId = await _repository.CreateProduct(p);
                    return CreatedAtAction("PostProduct", new { status = 200, msg = "Product Added", payload = productWithId });
                }
                else
                {
                    _log4net.Info("Model is not valid in PostProduct");
                    return BadRequest();
                }
            }
            catch(Exception e)
            {
                _log4net.Error("Database Error");
                return StatusCode(500);
            }
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            try
            {
                _log4net.Info("DeleteProduct Method Called");
                var product = await _repository.DeleteProduct(id);
                if (product == null)
                {
                    return NotFound();
                }

                return Ok(new { status = 200, msg = "Deleted Successfully", payload = product });
            }
            catch
            {
                _log4net.Error("Database Error");
                return StatusCode(500);
            }
        }

        
    }
}
