using WebApplication1.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data.Services
{
    public class ProductService
    {
        // Метод для добавления нового товара
        public async Task<Product> AddProduct(Product product)
        {
            DataSource.GetInstance().Products.Add(product);
            return await Task.FromResult(product);  // Возвращаем добавленный товар
        }

        // Метод для получения списка всех товаров
        public async Task<List<Product>> GetProducts()
        {
            return await Task.FromResult(DataSource.GetInstance().Products);  // Возвращаем список товаров
        }

        // Метод для получения товара по ID
        public async Task<Product?> GetProduct(int id)
        {
            var product = DataSource.GetInstance().Products.FirstOrDefault(p => p.Id == id);
            return await Task.FromResult(product);  // Возвращаем товар или null, если не найден
        }

        // Метод для обновления товара
        public async Task<Product?> UpdateProduct(Product newProduct)
        {
            var product = DataSource.GetInstance().Products.FirstOrDefault(p => p.Id == newProduct.Id);
            if (product != null)
            {
                product.Name = newProduct.Name;
                product.Description = newProduct.Description;
                product.Price = newProduct.Price;
                product.StockQuantity = newProduct.StockQuantity;
                return await Task.FromResult(product);  // Возвращаем обновленный товар
            }
            return null;  // Возвращаем null, если товар не найден
        }

        // Метод для удаления товара
        public async Task<bool> DeleteProduct(int id)
        {
            var product = DataSource.GetInstance().Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                DataSource.GetInstance().Products.Remove(product);
                return await Task.FromResult(true);  // Возвращаем true при успешном удалении
            }
            return await Task.FromResult(false);  // Возвращаем false, если товар не найден
        }
    }

}
