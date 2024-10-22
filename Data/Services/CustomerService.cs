using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.DTOs;
using WebApplication1.Data.Models;

namespace WebApplication1.Data.Services
{
    public class CustomerService
    {
        private readonly EducationContext _context;

        public CustomerService(EducationContext context)
        {
            _context = context;
        }

        // Добавление нового клиента через DTO
        public async Task<Customer?> AddCustomer(CustomerDTO customerDto)
        {
            var customer = new Customer
            {
                FullName = customerDto.FullName,
                Email = customerDto.Email,
                Address = customerDto.Address
            };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        // Получение всех клиентов
        public async Task<List<Customer>> GetCustomers()
        {
            return await _context.Customers.Include(c => c.Orders).ToListAsync();
        }

        // Получение клиента по ID
        public async Task<Customer?> GetCustomer(int id)
        {
            return await _context.Customers.Include(c => c.Orders).FirstOrDefaultAsync(c => c.Id == id);
        }

        // Обновление клиента через DTO
        public async Task<Customer?> UpdateCustomer(CustomerDTO customerDto)
        {
            var customer = await _context.Customers.FindAsync(customerDto.Id);
            if (customer == null) return null;

            customer.FullName = customerDto.FullName;
            customer.Email = customerDto.Email;
            customer.Address = customerDto.Address;
            await _context.SaveChangesAsync();
            return customer;
        }

        // Удаление клиента по ID
        public async Task<bool> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }
    }


}
