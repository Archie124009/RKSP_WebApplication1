using WebApplication1.Data.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.DTOs;

namespace WebApplication1.Data.Services
{
    public class ProductService
    {
        private readonly EducationContext _context;

        public ProductService(EducationContext context)
        {
            _context = context;
        }

        // Добавление нового товара через DTO
        public async Task<Product?> AddProduct(ProductDTO productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        // Получение всех товаров
        public async Task<List<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // Получение всех товаров по ID
        public async Task<Product?> GetProduct(int id)
        {
            return await _context.Products.Include(p => p.Orders).FirstOrDefaultAsync(p => p.Id == id);
        }

        // Обновление товара через DTO
        public async Task<Product?> UpdateProduct(ProductDTO productDto)
        {
            var product = await _context.Products.FindAsync(productDto.Id);
            if (product == null) return null;

            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            await _context.SaveChangesAsync();
            return product;
        }

        // Удаление товара по ID
        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }


}
