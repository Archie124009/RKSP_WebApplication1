using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.DTOs;
using WebApplication1.Data.Models;
using WebApplication1.Data.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        // Получить все товары
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productService.GetProducts();
            if (products == null || products.Count == 0)
            {
                return NoContent();  // Возвращаем 204, если товары не найдены
            }
            return Ok(products);  // Возвращаем 200 и список товаров
        }

        // Получить товар по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetProduct(id);
            if (product == null)
            {
                return NotFound();  // Возвращаем 404, если товар не найден
            }
            return Ok(product);  // Возвращаем 200 и товар
        }

        // Добавить новый товар
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromBody] ProductDTO product)
        {
            var newProduct = await _productService.AddProduct(product);
            return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, newProduct);  // Возвращаем 201 и созданный товар
        }

        // Обновить товар
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> PutProduct(int id, [FromBody] ProductDTO product)
        {
            if (id != product.Id)
            {
                return BadRequest("ID товара не совпадает");  // Возвращаем 400, если ID не совпадают
            }

            var updatedProduct = await _productService.UpdateProduct(product);
            if (updatedProduct == null)
            {
                return NotFound();  // Возвращаем 404, если товар не найден
            }

            return Ok(updatedProduct);  // Возвращаем 200 и обновленный товар
        }

        // Удалить товар
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var success = await _productService.DeleteProduct(id);
            if (!success)
            {
                return NotFound();  // Возвращаем 404, если товар не найден
            }
            return Ok();  // Возвращаем 200 при успешном удалении
        }
    }


}
