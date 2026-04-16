using Botaniqa.Api.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Cryptography.Xml;

namespace Botaniqa.Api.Controller
{
    [Route(template: "api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // In-memory storage for products (for demonstration purposes)
        private static List<Product> _products = new();

        private static int _nextId = 1;

        [HttpGet(template: "all")]
        public IActionResult GetAllProducts()
        {
            return Ok(_products);
        }

        [HttpGet(template: "{id}")]

        public IActionResult GetUserByProducts(int id)
        {

            var product = _products.FirstOrDefault(u => u.Id == id);

            if (product == null)
            {
                return NotFound(new { Message = $"Product with ID {id} not found" });
            }

            return Ok(product);
        }
        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product request)
        {
            var product = new Product
            {
                Id = _nextId++,
                ProductName = request.ProductName,
                Description = request.Description,
                Price = request.Price,
                Image = request.Image,
                IsAvailable = request.IsAvailable,
                Currency = request.Currency
            };

            _products.Add(product);
            return Created($"api/users/{product.Id}", product);
        }

        [HttpPut(template: "{id}")]

        public IActionResult UpdateProduct(int id, [FromBody] Product updatedUser)
        {
            var existingProduct = _products.FirstOrDefault(u => u.Id == id);

            if (existingProduct == null)
            {
                return NotFound(new { Message = $"User with ID {id} not found" });
            }
            existingProduct.ProductName = updatedUser.ProductName;
            existingProduct.Description = updatedUser.Description;
            existingProduct.Price = updatedUser.Price;
            existingProduct.Image = updatedUser.Image;
            existingProduct.IsAvailable = updatedUser.IsAvailable;
            existingProduct.Currency = updatedUser.Currency;

            return Ok(existingProduct);
        }


        [HttpDelete(template: "{id}")]

        public IActionResult DeleteProduct(int id)
        {
            var product = _products.FirstOrDefault(u => u.Id == id);

            if (product == null)
            {
                return NotFound(new { Message = $"Product with ID {id} not found" });
            }
            _products.Remove(product);

            return NoContent();
        }
    }
}
