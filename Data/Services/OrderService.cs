using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.DTOs;
using WebApplication1.Data.Models;

namespace WebApplication1.Data.Services
{
    public class OrderService
    {
        private readonly EducationContext _context;

        public OrderService(EducationContext context)
        {
            _context = context;
        }

        // Добавление нового заказа через DTO
        public async Task<Order?> AddOrder(OrderDTO orderDto)
        {
            var order = new Order
            {
                OrderDate = DateTime.Now,
                TotalAmount = orderDto.TotalAmount,
                Customers = (IEnumerable<Customer>)await _context.Customers.FindAsync(orderDto.CustomerId),
                Products = await _context.Products.Where(p => orderDto.ProductIds.Contains(p.Id)).ToListAsync()
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        // Получение всех заказов
        public async Task<List<Order>> GetOrders()
        {
            return await _context.Orders.Include(o => o.Customers).Include(o => o.Products).ToListAsync();
        }

        // Получение заказа по ID
        public async Task<Order?> GetOrder(int id)
        {
            return await _context.Orders.Include(o => o.Customers).FirstOrDefaultAsync(o => o.Id == id);
        }

        // Обновление заказа через DTO
        public async Task<Order?> UpdateOrder(OrderDTO orderDto)
        {
            var order = await _context.Orders.FindAsync(orderDto.Id);
            if (order == null) return null;

            order.TotalAmount = orderDto.TotalAmount;
            order.Products = await _context.Products.Where(p => orderDto.ProductIds.Contains(p.Id)).ToListAsync();
            await _context.SaveChangesAsync();
            return order;
        }

        // Удаление заказа
        public async Task<bool> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
